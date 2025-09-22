using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class Retry : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

