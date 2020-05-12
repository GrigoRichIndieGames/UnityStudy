using UnityEngine;
using UnityEngine.UI;


namespace GrigoRichIndieGames
{
    public sealed class PopupTextController : MonoBehaviour
    {
        public GameObject PopapTextPrefab;
        public Color TextColor;
        public Color OutlineColor;
        public float Lifetime;
        public int MaxFontSize;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PopUp("-100", Input.mousePosition);
            }
        }

        public void PopUp(string message, Vector3 position)
        {
            var popUpTextClone = Instantiate(PopapTextPrefab, position, Quaternion.identity, transform);

            SetTextSettings(popUpTextClone.transform.GetChild(0).GetComponent<Text>(), message);
            SetOutlineSettings(popUpTextClone.transform.GetChild(0).GetComponent<Outline>());

            Destroy(popUpTextClone, Lifetime);
        }

        private void SetTextSettings(Text text, string message)
        {
            text.color = TextColor;
            text.resizeTextMaxSize = MaxFontSize;
            text.text = message;
        }

        private void SetOutlineSettings(Outline outline)
        {
            outline.effectColor = OutlineColor;
        }
    }
}
