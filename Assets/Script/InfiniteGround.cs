using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    [Header("地面设置")]
    public Transform[] groundParts; // 地面对象数组
    public float scrollSpeed = 2f;  // 滚动速度
    public float groundWidth = 10f; // 单个地面的宽度

    void Update()
    {
        if (!(GameManager.Instance.currentState == GameState.GameOver))
        {
            // 遍历所有地面对象
            for (int i = 0; i < groundParts.Length; i++)
            {
                // 向左移动
                groundParts[i].Translate(Vector3.left * scrollSpeed * Time.deltaTime);

                // 检查是否移出屏幕左边
                if (groundParts[i].position.x < -groundWidth)
                {
                    // 计算最右边的位置
                    float rightmostX = GetRightmostPosition();

                    // 移动到最右边
                    Vector3 newPos = groundParts[i].position;
                    newPos.x = rightmostX + groundWidth;
                    groundParts[i].position = newPos;
                }
            }
        }

    }

    // 获取最右边地面的X位置
    float GetRightmostPosition()
    {
        float rightmost = groundParts[0].position.x;
        for (int i = 1; i < groundParts.Length; i++)
        {
            if (groundParts[i].position.x > rightmost)
            {
                rightmost = groundParts[i].position.x;
            }
        }
        return rightmost;
    }

}