using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 8;
    public AnimationCurve speed;
    public Projectile missilePrefab;

    public float missileAttackRate = 1.0f;

    public int amountKilled { get; private set; }
    public int ammountAlive => this.totalInvaders - this.amountKilled;
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

    private Vector3 _direction = Vector2.right;
    
    private void Awake(){
        for(int row = 0; row < this.rows; row++){
            float width = this.columns - 2.5f;
            float height = this.rows - 2.5f;
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 0.8f), 0.0f);

            for(int col = 0; col < this.columns; col++){

                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += col * 0.8f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start(){
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }


    private void Update(){
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform){
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }

            if(_direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.4f)){
                AdvanceRow();
            } else if(_direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.4f)){
                AdvanceRow();
            }
        }
    }


    private void AdvanceRow(){
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 0.5f;
        this.transform.position = position;
    }

    private void MissileAttack(){
        foreach (Transform invader in this.transform){
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }

            if(Random.value < (1.0f / (float)this.ammountAlive)){
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKilled(){
        this.amountKilled++;

        if(this.amountKilled >= this.totalInvaders) {
            SceneManager.LoadScene(1); //Redireciona para a cena de vitória;
        }
    }
}
