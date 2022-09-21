using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Match3NonPhys
{
    public class Piece : MonoBehaviour, IClickable
    {
        [field: SerializeField] private PieceType _type;
        [field: SerializeField] private GameObject _highlight;
        [field: SerializeField] private GameObject _visual;
        private Tween _currentTween;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(new Vector3(transform.position.x, transform.position.y - 3f, 0f));
                //Spin();
                //Despawn();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x, 0f, 0f);
            }
        }

        public void Move(Vector3 pos)
        {
            if (_currentTween != null && _currentTween.IsActive())
            {
                if (_currentTween.IsPlaying()) { return; }
            }
            _currentTween = transform.DOMove(pos, 0.5f).SetEase(Ease.OutBack);
        }
        public void Spin()
        {
            if (_currentTween != null && _currentTween.IsActive())
            {
                if (_currentTween.IsPlaying()) { return; }
            }
            _currentTween = _visual.transform.DORotate(new Vector3(0f, 360f, 0f), 0.25f, RotateMode.FastBeyond360);
        }
        public void Despawn()
        {
            _visual.transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InBack).OnComplete(()=> 
            {
                transform.DOKill();
                Destroy(gameObject);
            });

        }
        public void ToggleHighlight(bool b)
        {
            _highlight.SetActive(b);
        }
        public void ToggleHighlight()
        {
            _highlight.SetActive(!_highlight.activeSelf);
        }
        public void ClickAction()
        {
            ToggleHighlight();
        }
    }

    public enum PieceType
    {
        Red,
        Blue,
        Yellow,
        Green,
        Purple
    }
}