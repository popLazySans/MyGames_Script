using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderFollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform component
    public Slider HPslider; // Reference to the Slider component
    public TMP_Text Nametext;

    private RectTransform HPsliderRectTransform;
    private RectTransform NametextRectTransform;
    public PlayerRenderCharacter playerRender;

    private void Start()
    {
        HPsliderRectTransform = HPslider.GetComponent<RectTransform>();
        NametextRectTransform = Nametext.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(playerTransform.position);
            HPsliderRectTransform.position = new Vector2(screenPosition.x,screenPosition.y+60);
            NametextRectTransform.position = new Vector2(screenPosition.x, screenPosition.y + 100);
            HPslider.maxValue = playerRender.characterData.characters[playerRender.PlayerId].characteStat.HP;
            HPslider.value = playerRender.PlayerHP;
            Nametext.text = playerRender.PlayerName;
        }
    }
}
