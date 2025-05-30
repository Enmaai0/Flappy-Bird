using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed;
    private float leftBound = -4f;
    
    public void Initialize(float pipeSpeed)
    {
        speed = pipeSpeed;
    }
    
    void Update()
    {
        // 向左移动
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        // 超出左边界时销毁
        if (transform.position.x < leftBound)
        {
            DestroyPipe();
        }
        
        // 游戏结束时停止移动
        if (GameManager.Instance.currentState == GameState.GameOver)
        {
            speed = 0;
        }
    }
    
    void DestroyPipe()
    {
        FindFirstObjectByType<PipeManager>().RemovePipe(gameObject);
        Destroy(gameObject);
    }
}
