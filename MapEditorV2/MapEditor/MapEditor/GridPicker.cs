using Projet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Projet
{
    public delegate void onCellSelect(int ID, int pLine, int pColumn);

    class Cell // cellule
    {
        public bool Selected;
        public GCRectangle RectCell;
        public GCRectangle RectSelection;
        public int ID;
        public Texture2D Texture;


        public Cell(Game pGame, int pPosX, int pPosY, int pW, int pH) // Constructeur
        {
            Selected = false;
            RectCell = new GCRectangle(pGame, GCRectangle.Type.outline,
                       pPosX,
                       pPosY,
                       pW,
                       pH,
                       Color.Black,
                       Color.White
                       ); // création du rectangle
            RectSelection = new GCRectangle(pGame, GCRectangle.Type.outline,
                       pPosX-1,
                       pPosY-1,
                       pW+2,
                       pH+2,
                       Color.Black,
                       Color.Red
                       ); // création du rectangle de séléction

        }
        public void Select() //Séléction de cellule
        {
            Selected = !Selected; // inverse la valeur booléenne
        }
        public void Draw(SpriteBatch pSpritBatch)
        {
            RectCell.Draw(pSpritBatch);
            if (Selected)
            {
                RectSelection.Draw(pSpritBatch);
            }
        }
    }
    class GridPicker
    {
        private Cell[,] gridCell; // Cellule du tableau
        public Cell currentCell;
        public onCellSelect SelectionChanged;

        MouseState oldMouseState;

        public GridPicker(Game pGame, int pLines, int pColumns, int pW, int pH, int pEspace, Vector2 pPosition) // constructeur, nb de ligne, nb de colone, largeur, hauteur, espacement, position
        {
            gridCell = new Cell[pLines, pColumns];
            for (int l = 0; l < pLines; l++)
            {
                for (int c = 0; c < pColumns; c++)
                {
                    Cell myCell = new Cell(pGame, (c * (pW + pEspace)) + (int)pPosition.X,
                                                  (l * (pH + pEspace)) + (int)pPosition.Y,
                                                  24,
                                                  24);                  

                    gridCell[l, c] = myCell; //Stockage de la cellule
                }
            }

            oldMouseState = Mouse.GetState();
        }


        public void SetTexture(int pLine, int pColumn, Texture2D pTexture, int pID)
        {
            if(pLine < gridCell.GetLength(0)&& pColumn < gridCell.GetLength(1) && pLine >= 0 && pColumn >= 0)
            {
                gridCell[pLine, pColumn].Texture = pTexture;
                gridCell[pLine, pColumn].ID = pID;
            }
            else
            {
                Debug.WriteLine("Values out of bound for GridPicker.SetTexture");
            }
        }

        private void UnSelectAll()
        {
            for (int l = 0; l < gridCell.GetLength(0); l++)
            {
                for (int c = 0; c < gridCell.GetLength(1); c++)
                {
                    Cell myCell = gridCell[l, c];
                    if (myCell.Selected)
                    {                       
                        myCell.Select();                        
                    }
                }
            }
        }

        public void Update()
        {
            MouseState newMouseState = Mouse.GetState();

            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) // vérifier que c'est le 1er clique
            {
                // Parcours et test toutes les cellules
                for (int l = 0; l < gridCell.GetLength(0); l++)
                {
                    for (int c = 0; c < gridCell.GetLength(1); c++)
                    {
                        // Vérifie si le clic est à l'interieur
                        Cell myCell = gridCell[l, c];
                        if (myCell.RectCell.Rect.Contains(newMouseState.Position))
                        {
                            UnSelectAll();
                            myCell.Select();
                            SelectionChanged.Invoke(myCell.ID, l, c);
                            currentCell = myCell;

                        }
                    }
                }
            }
            oldMouseState = newMouseState;
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            for (int l = 0; l < gridCell.GetLength(0); l++)
            {

                for (int c = 0; c < gridCell.GetLength(1); c++)
                {
                    Cell myCell = gridCell[l, c];
                    myCell.Draw(pSpriteBatch);
                    if (myCell.Texture != null)
                    {
                        pSpriteBatch.Draw(myCell.Texture, new Vector2(myCell.RectCell.Rect.X, myCell.RectCell.Rect.Y), Color.White);
                    }
                }
            }
        }
    }
}
