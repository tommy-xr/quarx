//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//using Sxe.Engine.Input;
//using Sxe.Engine.UI;

//namespace Sxe.Engine.Utilities.Console
//{
//    public class ConsoleForm : Form
//    {
//        static Point location = new Point(150, 50);
//        static Point size = new Point(300, 300);
//        const int MaxLines = 500;
//        const int ButtonWidth = 70;


//        TextBox consoleText;
//        TextBox commandText;
//        Button commandButton;

//        Button graphicalTestButton;
//        Button unitTestButton;

//        ConsoleEventArgs consoleArgs = new ConsoleEventArgs();
//        IGameScreenService gameScreenService;


//        public event EventHandler<ConsoleEventArgs> ExecuteConsoleCommand;

//        public TextBox ConsoleTextBox
//        {
//            get { return consoleText; }
//        }



//        public void WriteLine(string text)
//        {
//            commandText.Text += text;
//        }

//        //public ConsoleForm(IScheme scheme, IGameScreenService screenService)
//        //    : base(location, size, scheme)
//        //{
//        //    gameScreenService = screenService;

//        //    int height = scheme.FontHeight;
//        //    consoleText = new TextBox(ClientArea, new Point(5, 5), new Point(ClientArea.Size.X - 10, ClientArea.Size.Y - 2 * (height + 10)), scheme, MaxLines);
//        //    consoleText.ReadOnly = true;

//        //    commandText = new TextBox(ClientArea, new Point(5, ClientArea.Size.Y - 2 * (height + 5)), new Point(ClientArea.Size.X - ButtonWidth - 5, height), scheme, 1);

//        //    commandButton = new Button(ClientArea, new Point(ClientArea.Size.X - ButtonWidth, ClientArea.Size.Y - 2 * (height + 5)), new Point(ButtonWidth, height),
//        //        scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage, scheme.CommandButtonClickImage, scheme.DefaultFontColor,
//        //        BorderPanelMode.Resize, scheme);
//        //    commandButton.Text = "Submit";
//        //    commandButton.MouseClick += OnSubmitPressed;

//        //    graphicalTestButton = new Button(ClientArea, new Point(5, ClientArea.Size.Y - (height + 5)),
//        //        new Point(ClientArea.Size.X / 2 - 5, height), scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage,
//        //        scheme.CommandButtonClickImage, scheme.DefaultFontColor, BorderPanelMode.Resize, scheme);

//        //    unitTestButton = new Button(ClientArea, new Point(5 + ClientArea.Size.X / 2, ClientArea.Size.Y - (height + 5)),
//        //        new Point(ClientArea.Size.X / 2 - 5, height), scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage,
//        //        scheme.CommandButtonClickImage, scheme.DefaultFontColor, BorderPanelMode.Resize, scheme);

//        //    unitTestButton.Text = "Unit Tests";
//        //    graphicalTestButton.Text = "Graphical Tests";

//        //    unitTestButton.MouseClick += OnClickUnitTest;
//        //    graphicalTestButton.MouseClick += OnClickGraphicalTest;

//        //    Text = "Console";
//        //    Name = "Console";
//        //}

//        public ConsoleForm(IGameScreenService screenService)
//            : base(screenService.Schemes)
//        {
//            this.Size = size;
//            this.Location = location;

//            gameScreenService = screenService;

//            int height = 24;
//            //consoleText = new TextBox(ClientArea, new Point(5, 5), new Point(ClientArea.Size.X - 10, ClientArea.Size.Y - 2 * (height + 10)), scheme, MaxLines);
//            consoleText = new TextBox(MaxLines);
//            consoleText.Parent = ClientArea;
//            consoleText.Location = new Point(5, 5);
//            consoleText.Size = new Point(ClientArea.Size.X - 10, ClientArea.Size.Y - 2 * (height + 10));
//            consoleText.ReadOnly = true;

//            //commandText = new TextBox(ClientArea, new Point(5, ClientArea.Size.Y - 2 * (height + 5)), new Point(ClientArea.Size.X - ButtonWidth - 5, height), scheme, 1);
//            commandText = new TextBox(1);
//            commandText.Parent = ClientArea;
//            commandText.Location = new Point(5, ClientArea.Size.Y - 2 * (height + 5));
//            commandText.Size = new Point(ClientArea.Size.X - ButtonWidth - 5, height);


//            //commandButton = new Button(ClientArea, new Point(ClientArea.Size.X - ButtonWidth, ClientArea.Size.Y - 2 * (height + 5)), new Point(ButtonWidth, height),
//            //    scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage, scheme.CommandButtonClickImage, scheme.DefaultFontColor,
//            //    BorderPanelMode.Resize, scheme);
//            commandButton = new Button();
//            commandButton.Parent = ClientArea;
//            commandButton.Location = new Point(ClientArea.Size.X - ButtonWidth, ClientArea.Size.Y - 2 * (height + 5));
//            commandButton.Size = new Point(ButtonWidth, height);
//            commandButton.Text = "Submit";
//            commandButton.MouseClick += OnSubmitPressed;

//            //graphicalTestButton = new Button(ClientArea, new Point(5, ClientArea.Size.Y - (height + 5)),
//            //    new Point(ClientArea.Size.X / 2 - 5, height), scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage,
//            //    scheme.CommandButtonClickImage, scheme.DefaultFontColor, BorderPanelMode.Resize, scheme);
//            graphicalTestButton = new Button();
//            graphicalTestButton.Parent = ClientArea;
//            graphicalTestButton.Location = new Point(5, ClientArea.Size.Y - (height + 5));
//            graphicalTestButton.Size = new Point(ClientArea.Size.X / 2 - 5, height);

//            //unitTestButton = new Button(ClientArea, new Point(5 + ClientArea.Size.X / 2, ClientArea.Size.Y - (height + 5)),
//            //    new Point(ClientArea.Size.X / 2 - 5, height), scheme.DefaultFont, scheme.CommandButtonImage, scheme.CommandButtonOverImage,
//            //    scheme.CommandButtonClickImage, scheme.DefaultFontColor, BorderPanelMode.Resize, scheme);
//            unitTestButton = new Button();
//            unitTestButton.Parent = ClientArea;
//            unitTestButton.Location = new Point(5 + ClientArea.Size.X / 2, ClientArea.Size.Y - (height + 5));
//            unitTestButton.Size = new Point(ClientArea.Size.X / 2 - 5, height);

//            unitTestButton.Text = "Unit Tests";
//            graphicalTestButton.Text = "Graphical Tests";

//            unitTestButton.MouseClick += OnClickUnitTest;
//            graphicalTestButton.MouseClick += OnClickGraphicalTest;

//            Text = "Console";
//            Name = "Console";

//            //ClientArea.ApplyScheme(screenService.Schemes.DefaultScheme);
//        }

//        //TODO:
//        //Fix these so that we can reference the screen that contains the form elements

//        void OnClickGraphicalTest(object sender, EventArgs args)
//        {
//            FormCollection.SetFormVisible("GTF", true);
//            //FormScreen fs = gameScreenService.CurrentScreen as FormScreen;
//            //if(fs != null)
//            //fs.Forms.SetFormVisible("GTF", true);
//        }

//        void OnClickUnitTest(object sender, EventArgs args)
//        {
//            FormCollection.SetFormVisible("UTF", true);
//            //FormScreen fs = gameScreenService.CurrentScreen as FormScreen;
//            //if(fs != null)
//            //fs.Forms.SetFormVisible("UTF", true);
//        }

//        void OnSubmitPressed(object sender, EventArgs value)
//        {
//            if (ExecuteConsoleCommand != null)
//            {
//                consoleArgs.Command = commandText.Text;
//                ExecuteConsoleCommand(this, consoleArgs);
//                commandText.Text = "";
//            }
//        }
//    }
//}
