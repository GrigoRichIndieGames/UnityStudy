using UnityEngine;


namespace GrigoRichIndieGames
{
    public class PuffVFXController : MonoBehaviour
    {
        #region Fields

        public float LifeTime = 1;

        #endregion


        #region UnityMethods

        private void Start()
        {
            Destroy(gameObject, LifeTime);
        }

        #endregion
    }
}
