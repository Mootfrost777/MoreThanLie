using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using MainGame.Classes;
using System;

namespace MainGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static string DataTransferFileName = "DataTransfer.txt";
        public static string DataFileName = "../../../../MoreThanLight Launcher/bin/Debug/net5.0-windows/accounts.txt";

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            string[] data = ImportData();
            if (CheckData(data) == true)
            {
                Player player = new Player(data[0]);
                File.WriteAllText(DataTransferFileName, String.Empty);
            }
            else this.Exit();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static string[] ImportData()
        {
            StreamReader streamReader = new StreamReader(DataTransferFileName);
            string[] data = streamReader.ReadLine().Split(' ');
            streamReader.Close();
            return data;
        }

        public static bool CheckData(string[] data)
        {
            string[] lines = File.ReadAllLines(DataFileName);
            string[] split = new string[1];
            foreach (string line in lines)
            {
                split = line.Split(' ');
                if (split[0] == data[0] && split[1] == data[1])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
