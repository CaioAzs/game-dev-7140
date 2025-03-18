using UnityEngine;

public class Invader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Sprite[] animationSprites;
    public float animationTime = 1.0f;

    public System.Action killed;
    public int points = 100;

    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;
    
    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Start(){
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite(){
        _animationFrame++;

        if(_animationFrame >= this.animationSprites.Length){
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Laser")){
            GameManager.Instance.AddPoints(points);
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
