using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Rullet
{
    public class SpinRullet : MonoBehaviour
    {
        public SpinState state;
        public Image spinBackground;
        public List<Sprite> sprites;

        public int spinCount;
        public Button spinButton, backButton;
        private Coroutine _coroutine;

        public bool isActive;

        private void Awake()
        {
            EventController.OnSpinStateChange += ChangeState;
        }

        private void OnValidate()
        {
            spinButton.onClick.AddListener(()=> Spin());
        }

        private void OnDestroy()
        {
            EventController.OnSpinStateChange -= ChangeState;
        }
    
        private void Spin()
        {
            var randomLoopCount = Random.Range(50, 101);
            var randomEndPosition = Random.Range(1, 8);
            spinButton.interactable = false;
            backButton.interactable = false;
        

            transform.DORotate(new Vector3(0, 0, 360),
                    .1f, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(randomLoopCount, LoopType.Incremental)
                .OnComplete(() =>
                {
                    var targetRotation = new Vector3(0, 0, randomEndPosition * 45);
                    transform.DORotate(targetRotation, 1f)
                        .SetEase(Ease.Linear)
                        .OnUpdate(()=>EventController.OnSpinStart?.Invoke(transform.localRotation.z))
                        .OnComplete(() =>
                        {
                            spinCount++;
                            EventController.OnSpinEnd?.Invoke(spinCount);
                            spinButton.interactable = true;
                        });
                });

            if (spinCount == 31)
            {
                spinCount = 2;
            }
        }
    

        void ChangeState(SpinState state)
        {
            this.state = state;
            switch (state)
            {
                case SpinState.Normal :
                    spinBackground.sprite = sprites[0];
                    break;
                case SpinState.Safe :
                    spinBackground.sprite = sprites[1];
                    backButton.interactable = true;
                    break;
                case SpinState.Super :
                    spinBackground.sprite = sprites[2];
                    backButton.interactable = true;
                    break;
            }
        }
    }

    public enum SpinState
    {
        Normal,
        Safe,
        Super
    }
}