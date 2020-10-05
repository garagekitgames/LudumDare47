using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace DamageEffect
{
    [AddComponentMenu("DamageEffect")]
    public class DamageEffectScript : MonoBehaviour
    {

        [SerializeField]
        private Material _damageMaterial;
        private Dictionary<Renderer, Material> _defaultMaterials;
        private Dictionary<Renderer, Material> _damageMaterials;
        private Renderer[] _rendereres;

        private static Dictionary<Texture, Material> _damageMaterialsCache = new Dictionary<Texture, Material>();

        private void Start()
        {
            _rendereres = GetComponentsInChildren<Renderer>();
            _defaultMaterials = _rendereres.ToDictionary(p => p, p => p.sharedMaterial);
            _damageMaterials = new Dictionary<Renderer, Material>();
            foreach (var sr in _defaultMaterials)
            {
                if (sr.Key is SpriteRenderer)
                {
                    _damageMaterials[sr.Key] = _damageMaterial;
                }
                else
                {
                    Material mat;
                    Texture tex = sr.Key.sharedMaterial.mainTexture;

                    if (!_damageMaterialsCache.TryGetValue(tex, out mat))
                    {
                        mat = Instantiate(_damageMaterial) as Material;
                        mat.mainTexture = tex;
                        _damageMaterialsCache[tex] = mat;
                    }
                    _damageMaterials[sr.Key] = mat;
                }
            }
        }

        /// <summary>
        /// Starts the effect after waitSeconds and blinks for blinkSeconds 
        /// </summary>
        /// <param name="waitSeconds">delay in seconds</param>
        /// <param name="blinkSeconds">duration in seconds</param>
        public void Blink(float waitSeconds, float blinkSeconds)
        {
            if (_defaultMaterials == null || _defaultMaterials.Count == 0 || _damageMaterial == null)
            {
                return;
            }

            StopCoroutine("BlinkCoroutine");
            StartCoroutine(BlinkCoroutine(waitSeconds, blinkSeconds));
        }

        private IEnumerator BlinkCoroutine(float waitSeconds, float blinkSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);

            foreach (var sr in _defaultMaterials)
            {
                sr.Key.sharedMaterial = _damageMaterials[sr.Key];
            }

            yield return new WaitForSeconds(blinkSeconds);

            foreach (var sr in _defaultMaterials)
            {
                sr.Key.sharedMaterial = sr.Value;
            }
        }
    }
}
