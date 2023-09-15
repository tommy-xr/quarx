using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{

    //public class EditablePanel : Panel
    //{
    //    public EditablePanel(IPanelContainer parent, Point position, Point size, UIImage image)
    //        : base(parent, position, size, image)
    //    {
    //    }
    //}

    /// <summary>
    /// This is the base class for all editable UI elements
    /// An editable UI element supports an editor mode,
    /// where you can move, resize, and modify elements
    /// </summary>

    public class EditablePanel : DraggablePanel
    //public class EditablePanel : DraggablePanel, IEditable
    {
        //const int offset = 10;
        //const int resizeSize = 9;

        //bool isEditing = true;

        ////The actual panel - the editor panel adds some extra stuff around it
        ////Panel editorPanel;
        //DraggablePanel topResizeButton;
        //DraggablePanel leftResizeButton;
        //DraggablePanel rightResizeButton;
        //DraggablePanel bottomResizeButton;

        ////Fields for handling moving
        //bool isMoving = false;
        //Point cursorStartPosition = new Point(-1, -1);
        //Point startPosition = new Point(-1, -1);

        //public event EventHandler<EventArgs> EditingChanged;


        //public bool Editable { get { return true; } }

        //public bool IsEditing
        //{
        //    get { return isEditing; }
        //    set 
        //    { 
        //        isEditing = value;

        //        if (EditingChanged != null)
        //            EditingChanged(this, EventArgs.Empty);

        //        //if(editorPanel != null)
        //        //editorPanel.Visible = value;
        //    }
        //}

        //public override bool CanDrag
        //{
        //    get
        //    {
        //        return isEditing;
        //    }
        //}



        //public EditablePanel()
        //{
        //    IsEditing = false;

        //    //Create the actual panel
        //    //UIImage temp = new UIImage(image.Value);
        //    //temp.Color = Color.Red;
        //    //editorPanel = new Panel(parent, new Point(position.X - offset, position.Y - offset), 
        //    // new Point(size.X + offset *2, size.Y + offset * 2), temp);
        //    //editorPanel.Visible = false;

        //    //UIImage blank = new UIImage(scheme.Content.Load<Texture2D>("blank"));

        //    UIImage blank = null;

        //    //TODO:
        //    //When parent is assigned, assign resize buttons to them
        //    topResizeButton = new DraggablePanel();
        //    topResizeButton.Parent = this;
        //    topResizeButton.Size = new Point(resizeSize, resizeSize);
        //    topResizeButton.Image = blank;
        //    topResizeButton.DragMultiplier = Vector2.UnitY;
        //    topResizeButton.Drag += ResizeVertical;


        //    leftResizeButton = new DraggablePanel();
        //    leftResizeButton.Parent = this;
        //    leftResizeButton.Size = new Point(resizeSize, resizeSize);
        //    leftResizeButton.Image = blank;
        //    leftResizeButton.DragMultiplier = Vector2.UnitX;
        //    leftResizeButton.Drag += ResizeHorizontal;

        //    bottomResizeButton = new DraggablePanel();
        //    bottomResizeButton.Parent = this;
        //    bottomResizeButton.Size = new Point(resizeSize, resizeSize);
        //    bottomResizeButton.Image = blank;
        //    bottomResizeButton.DragMultiplier = Vector2.UnitY;
        //    bottomResizeButton.Drag += ResizeVertical;

        //    rightResizeButton = new DraggablePanel();
        //    rightResizeButton.Parent = this;
        //    rightResizeButton.Size = new Point(resizeSize, resizeSize);
        //    rightResizeButton.Image = blank;
        //    rightResizeButton.DragMultiplier = Vector2.UnitX;
        //    rightResizeButton.Drag += ResizeHorizontal;

        //    ResetSizeHandles();
        //    SetEditorVisible(false);

        //    this.Drag += OnDrag;
        //    this.EditingChanged += OnEditingChanged;
        //    this.SizeChanged += OnResize;
        //    //this.MouseClick += OnClick;
        //    //this.MouseClickRelease += OnUnClick;
        //}

        //void OnEditingChanged(object sender, EventArgs args)
        //{
        //    SetEditorVisible(isEditing);
        //}

        //void SetEditorVisible(bool visible)
        //{
        //    topResizeButton.Visible = visible;
        //    bottomResizeButton.Visible = visible;
        //    leftResizeButton.Visible = visible;
        //    rightResizeButton.Visible = visible;
        //}

        //void OnClick(object sender, EventArgs args)
        //{
        //    if (isEditing)
        //    {
        //        SetEditorVisible(true);
        //    }
        //}

        //void OnUnClick(object sender, EventArgs args)
        //{
        //    if (isEditing)
        //    {
        //        SetEditorVisible(false);
        //    }
        //}

        //void OnResize(object sender, EventArgs args)
        //{
        //    ResetSizeHandles();
        //}

        //void OnDrag(object sender, EventArgs args)
        //{
        //    ResetSizeHandles();
        //}

        //void ResetSizeHandles()
        //{
        //    ResetVerticalSizeHandles();
        //    ResetHorizontalSizeHandles();
        //}

        ///// <summary>
        ///// Center the size handles around the object
        ///// </summary>
        //void ResetVerticalSizeHandles()
        //{
        //    int halfResize = resizeSize/2;
        //    topResizeButton.Location = new Point(Location.X + Size.X / 2 + halfResize, Location.Y -resizeSize);
        //    bottomResizeButton.Location = new Point(Location.X + Size.X / 2 + halfResize, Location.Y + Size.Y);


        //}
        //void ResetHorizontalSizeHandles()
        //{
        //    int halfResize = resizeSize / 2;
        //    leftResizeButton.Location = new Point(Location.X - resizeSize, Location.Y + Size.Y / 2 + halfResize);
        //    rightResizeButton.Location = new Point(Location.X + Size.X, Location.Y + Size.Y / 2 + halfResize);
        //}

        //public override void StartEdit()
        //{
        //    IsEditing = true;
        //    base.StartEdit();
        //}

        //public override void EndEdit()
        //{
        //    IsEditing = false;
        //    base.EndEdit();
        //}

        //public void ResizeVertical(object sender, EventArgs dragEvent)
        //{
        //    this.Location = new Point(this.Location.X, topResizeButton.Location.Y + resizeSize);
        //    this.Size = new Point(this.Size.X, bottomResizeButton.Location.Y - (topResizeButton.Location.Y + resizeSize));

        //    ResetHorizontalSizeHandles();
        //    //editorPanel.Size = new Point(editorPanel.Size.X, bottomResizeButton.Location.Y - topResizeButton.Location.Y);
        //}

        //public void ResizeHorizontal(object sender, EventArgs dragEvent)
        //{
        //    this.Location = new Point(leftResizeButton.Location.X + resizeSize, this.Location.Y);
        //    this.Size = new Point(rightResizeButton.Location.X - (leftResizeButton.Location.X + resizeSize), this.Size.Y);

        //    ResetVerticalSizeHandles();
        //}


    }
}
