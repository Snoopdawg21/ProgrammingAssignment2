using UnityEngine;
using DG.Tweening;

public class ChestInteractable : MonoBehaviour, IIInteractable
{

    private Tween loopTween;
    private Tween collectTween;
    
    void Start()
    {
        transform.DOScale(2f, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad).SetDelay(Random.Range(0.5f, 100));
    }
    
    public void OnInteract()
    {
        Debug.Log(gameObject.name);

        collectTween = transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });

    }

    public void OnHoverIn()
    {
        Debug.Log("fuck");
        Toast.instance.ShowToast("Press \"E\" to interact");
    }

    public void OnHoverOut()
    {
        Debug.Log("Shit");
        Toast.instance.HideToast();
    }

    void OnDestroy()
    {
        DOTween.Kill(this.gameObject);
    }
}
