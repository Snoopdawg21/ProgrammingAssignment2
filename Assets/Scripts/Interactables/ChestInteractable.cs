using UnityEngine;
using DG.Tweening;

public class ChestInteractable : MonoBehaviour, IIInteractable
{
    private AttackManager am;

    private Tween loopTween;
    private Tween collectTween;
    
    void Start()
    {
        transform.DOScale(2f, .5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad).SetDelay(Random.Range(0.5f, 100));

        if (am == null)
            am = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackManager>();
    }
    
    public void OnInteract()
    {
        Debug.Log(gameObject.name);

        collectTween = transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            transform.DOKill();
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
