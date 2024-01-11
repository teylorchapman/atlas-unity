using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
   public TextMeshProUGUI text;
   private int score = 0;

   public void ScoreIncrease()
   {
        score++;
        text.text = score.ToString();
   }
}
