using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;

    private GameManager gameManager;

    private int cost;

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private Image glowEffect;
    [SerializeField] private float lerpFactor = .1f;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        gameManager = FindAnyObjectByType<GameManager>();
        cost = gameObject.GetComponent<CardDisplay>().cardData.cost;
    }

    void Update()
    {
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if(!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                if(!Input.GetMouseButton(0))
                {
                    PlayCard();
                }
                break;

        }
    }

    private void TransitionToState0()
    {
        // if cancel: reset card in hand
        currentState = 0;
        rectTransform.localScale = originalScale;
        rectTransform.localPosition = originalPosition;
        rectTransform.localRotation = originalRotation;
        glowEffect.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalScale = rectTransform.localScale;
            originalRotation = rectTransform.localRotation;
            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(currentState == 2)
        {
            Vector2 localPointerPosition;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);
                if(rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3;
                    rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);                }
            }
        }
    }

    private void HandleHoverState()
    {
        glowEffect.gameObject.SetActive(true);
        // set color
        if(cost > gameManager.GetMoney())
        {
            glowEffect.color = new Color(1, 0, 0, 0.6f);
        }
        else
        {
            glowEffect.color = new Color(0, 1, 1, 0.6f);
        }
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;


        if(Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
        }
    }

    private void PlayCard()
    {
        gameManager.Play(gameObject);
    }
}
