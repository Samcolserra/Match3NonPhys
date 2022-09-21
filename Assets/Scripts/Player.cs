using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3NonPhys
{
    public class Player : MonoBehaviour
    {
        private Piece _selectedPiece;

        private List<Piece> _pieces = new List<Piece>();
        private bool done = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                MouseRay();
            }
            if(_pieces.Count >= 3)
            {
                if (!done)
                {
                    done = true;
                    foreach (Piece p in _pieces)
                    {
                        p.Spin();
                    }
                }
            }
        }

        private void MouseRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, 100)) { return; }
            IClickable clickable = hit.transform.GetComponent<IClickable>();

            if (clickable == null) { return; }
            clickable.ClickAction();

            Piece piece = hit.transform.GetComponent<Piece>();

            if (piece == null) { return; }
            if (_selectedPiece == null) { _selectedPiece = piece; return; }
            //_pieces.Add(piece);
            SwapPieces(piece);
        }
        private void SwapPieces(Piece p)
        {
            if (Vector3.Distance(p.transform.position, _selectedPiece.transform.position) > 1.1f)
            {
                p.ToggleHighlight(false);

                _selectedPiece.ToggleHighlight(false);
                _selectedPiece = null;
                return;
            }

            Vector3 pos = p.transform.position;

            p.ToggleHighlight(false);
            p.Move(_selectedPiece.transform.position);

            _selectedPiece.Move(pos);
            _selectedPiece.ToggleHighlight(false);
            _selectedPiece = null;
        }
    }

}