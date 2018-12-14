using Projet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projet
{
    class Tile
    {
        public int id;
        public Texture2D texture;

    }
    class MapEditor
    {
        private Game previewgame;
        public bool isActive { get; set; }
        private SpriteFont Font;

        private int[,] mapData;
        private List<Tile> lstTiles;

        private GridPicker TilePicker;
        private Vector2 TilePickerPosition;

        private GridPicker MapPicker;
        private Vector2 MapPickerPosition;
        
        const int TILESIZE = 24; // taille des tuiles

        public MapEditor(Game pGame, int pNbTiles, ref int[,] pMapData) //reçoit une instance du jeu
        {
            previewgame = pGame; // peut utiliser les ressources graphiques etc du jeu
            mapData = pMapData;
            Font = previewgame.Content.Load<SpriteFont>("pixelfont"); //Charge la police de caractère
            isActive = false;
            lstTiles = new List<Tile>();

            TilePickerPosition.X = 0;
            TilePickerPosition.Y = 20;

            MapPickerPosition.X = 0;
            MapPickerPosition.Y = TilePickerPosition.Y + TILESIZE + 5; // hauteur des tuiles

            TilePicker = new GridPicker(pGame, 1, pNbTiles, TILESIZE, TILESIZE, 3, TilePickerPosition);
            MapPicker = new GridPicker(pGame, pMapData.GetLength(0), pMapData.GetLength(1), TILESIZE, TILESIZE, 3, MapPickerPosition);

            TilePicker.SelectionChanged = onTileSelect;
            MapPicker.SelectionChanged = onMapSelect;
        }

        public void AddTile(int pID, Texture2D pTexture)
        {
            Tile myTile = new Tile
            {
                id = pID,
                texture = pTexture
            };
            lstTiles.Add(myTile);
            string str_id = pID.ToString();
           // Save(str_id);
            TilePicker.SetTexture(0, lstTiles.Count - 1, pTexture, pID); //nombre de cellule qu'il y a déjà -1
        }
        public void UpdateGrid()
        {
            for (int l = 0; l < mapData.GetLength(0); l++)
            {
                for (int c = 0; c < mapData.GetLength(1); c++)  
                {
                    MapPicker.SetTexture(l, c, lstTiles[mapData[l, c]].texture, mapData[l, c]);
                }
            }
        }


        public void Active()
        {
            isActive = !isActive;
        }

        public void onTileSelect(int pID, int pLine, int pColumn)
        {

        }

        public void onMapSelect(int pID, int pLine, int pColumn)
        {
            if (TilePicker.currentCell != null)
            {
                mapData[pLine, pColumn] = TilePicker.currentCell.ID;
                UpdateGrid();  // texture remise en fonction des valeurs dans les cellules
            }
        }

        public void Update()
        {
            TilePicker.Update();
            MapPicker.Update();
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            if (isActive)
            {
                pSpriteBatch.DrawString(Font, "INTEGRATED MAP EDITOR", new Vector2(0, 0), Color.White);

                TilePicker.Draw(pSpriteBatch);
                MapPicker.Draw(pSpriteBatch);
            }
        }
        /* SYSTEME DE SAUVEGARDE 
        public void Save(string sym)
        {
            // Create the XmlDocument.
            XmlDocument doc = new XmlDocument();

            // Add element.
            XmlElement newElem = doc.CreateElement("line");
            newElem.InnerText = sym;
            doc.DocumentElement.AppendChild(newElem);

            // Save the document to a file. White space is
            // preserved (no white space).
            doc.PreserveWhitespace = true;
            doc.Save("map.xml");

        } */

    }
}
