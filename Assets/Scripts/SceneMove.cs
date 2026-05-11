using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
   public void SceneTransition()
   {
       SceneManager.LoadScene(0);
   }
}
