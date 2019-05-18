using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class Booster : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Image m_image;
    RectTransform m_rectXform;
    Vector3 m_startPosition;
    Board m_board;
    Tile m_TileTarget;

    public static GameObject ActiveBooster;
    public Text instructionsText;
    public string instructions = "drag over game piece to remove";

    public bool isEnabled = false;
    public bool isDraggable = true;
    public bool isLocked = false;

    public List<CanvasGroup> canvasGroups;
    public UnityEvent boostEvent;
    public int boostTime = 15;

    void Awake()
    {
        m_image = GetComponent<Image>();
        m_rectXform = GetComponent<RectTransform>();
        m_board = Object.FindObjectOfType<Board>().GetComponent<Board>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EnableBooster(false);
    }

    public void EnableBooster(bool state)
    {
        isEnabled = state;

        if(state)
        {
            DisableOtherBoosters();
            Booster.ActiveBooster = gameObject;
        }
        else if(gameObject == Booster.ActiveBooster)
        {
            Booster.ActiveBooster = null;
        }

        m_image.color = (state) ? Color.white : Color.gray;

        if(instructionsText != null)
        {
            instructionsText.gameObject.SetActive(Booster.ActiveBooster != null);

            if(gameObject == Booster.ActiveBooster)
            {
                instructionsText.text = instructions;
            }
        }
    }

    void DisableOtherBoosters()
    {
        Booster[] allBoosters = Object.FindObjectsOfType<Booster>();

        foreach (Booster b in allBoosters)
        {
            if (b != this)
            {
                b.EnableBooster(false);
            }
        }
    }

    public void ToggleBooster()
    {
        EnableBooster(!isEnabled);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isEnabled && isDraggable && !isLocked)
        {
            m_startPosition = gameObject.transform.position;
            EnableCanvasGroups(false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isEnabled && isDraggable && !isLocked)
        {
            Vector3 onscreenPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rectXform, eventData.position,
                                                                        Camera.main, out onscreenPosition);
            gameObject.transform.position = onscreenPosition;

            RaycastHit2D hit2D = Physics2D.Raycast(onscreenPosition, Vector3.forward, Mathf.Infinity);

            if(hit2D.collider != null)
            {
                m_TileTarget = hit2D.collider.GetComponent<Tile>();
            }
            else
            {
                m_TileTarget = null;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isEnabled && isDraggable && !isLocked)
        {
            gameObject.transform.position = m_startPosition;
            EnableCanvasGroups(true);

            if(m_board != null && m_board.isRefilling)
            {
                return;
            }

            if(m_TileTarget != null)
            {
                if(boostEvent != null)
                {
                    boostEvent.Invoke();
                }

                EnableBooster(false);

                m_TileTarget = null;
                Booster.ActiveBooster = null;
            }
        }
    } 

    void EnableCanvasGroups(bool state)
    {
        if(canvasGroups != null && canvasGroups.Count > 0)
        {
            foreach(CanvasGroup cGroup in canvasGroups)
            {
                if(cGroup != null)
                {
                    cGroup.blocksRaycasts = state;
                }
            }
        }
    }

    public void RemoveOneGamePiece()
    {
        if(m_board != null && m_TileTarget != null)
        {
            m_board.boardClearer.ClearAndRefillBoard(m_TileTarget.xIndex, m_TileTarget.yIndex);
        }
    }

    public void AddTime()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.AddTime(boostTime);
        }
    }

    public void DropColorBomb()
    {
        if(m_board != null && m_TileTarget != null)
        {
            m_board.boardFiller.MakeColorBombBooster(m_TileTarget.xIndex, m_TileTarget.yIndex);
        }
    }
}
