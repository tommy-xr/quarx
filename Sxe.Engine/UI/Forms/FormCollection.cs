using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Sxe.Engine.Input;

namespace Sxe.Engine.UI
{
    public class FormCollection : IInputEventReceiver
    {
        //static List<Form> globalForms;
        //public static List<Form> GlobalForms
        //{
        //    get { if (globalForms == null) globalForms = new List<Form>(); return globalForms; }
        //}



        List<OldForm> forms;

        public FormCollection()
        {
            forms = new List<OldForm>();
        }

        public bool HandleEvent()
        {
            return false;
        }

        public void AddForm(OldForm add)
        {
            forms.Add(add);
            add.FormCollection = this;
        }

        //TODO:
        //OK to remove these? The global form collection has been separated
        //This makes the code clearer and with less border cases for activating/deactivating forms

        ///// <summary>
        ///// This is called when the forms collection is activated, and needs to pull in
        ///// global forms
        ///// </summary>
        //public void Activate()
        //{
        //    for (int i = 0; i < GlobalForms.Count; i++)
        //    {
        //        forms.Add(GlobalForms[i]);
        //    }

        //    GlobalForms.Clear();
        //}

        //public void Deactivate()
        //{
        //    for (int i = 0; i < forms.Count; i++)
        //    {
        //        if (!forms[i].IsLocalOnly)
        //        {
        //            GlobalForms.Add(forms[i]);
        //        }
        //    }
        //    foreach (Form f in globalForms)
        //    {
        //        if (forms.Contains(f))
        //            forms.Remove(f);
        //    }

        //}

        /// <summary>
        /// Makes the specified form visible. Returns ture if form is found, false otherwise
        /// </summary>
        /// <param name="name"></param>
        public bool SetFormVisible(string name, bool visible)
        {
            OldForm foundForm = null;
            for (int i = 0; i < forms.Count; i++)
            {
                if (forms[i].Name == name)
                {
                    foundForm = forms[i];
                    forms[i].Visible = visible;
                }
            }

            if (foundForm != null)
            {
                forms.Remove(foundForm);

                if (visible)
                    forms.Insert(0, foundForm);
                else
                    forms.Add(foundForm);
                return true;
            }
            else
                return false;
        }

        public OldForm GetFormByName(string name)
        {
            for (int i = 0; i < forms.Count; i++)
            {
                if (forms[i].Name == name)
                {
                    return forms[i];
                }
            }
            return null;
        }

        public void Update(GameTime time)
        {
            for (int i = 0; i < forms.Count; i++)
            {
                if(forms[i].Visible)
                forms[i].Update(time);
            }
        }

        public void DrawForms(SpriteBatch sb)
        {
            //Paint each form
            foreach (OldForm f in forms)
            {
                if(f.Visible)
                f.Paint(sb, Point.Zero, Vector2.One);
            }
        }

        public void Draw(SpriteBatch sb)
        {

            sb.Begin();
            //Now, draw each form, in reverse order
            for (int i = forms.Count - 1; i >= 0; i--)
            {
                //TODO: Make this a function that forms have control over
                if (forms[i].Visible)
                {
                    Color c = Color.White;

                    if (i != 0)
                        c = new Color(c.R, c.G, c.B, 150);

                    if(forms[i].FormTexture != null)
                    sb.Draw(forms[i].FormTexture, new Rectangle(forms[i].Location.X, forms[i].Location.Y,
                        forms[i].Size.X, forms[i].Size.Y), c);
                }
            }
            sb.End();
        }

        public bool HandleEvent(InputEventArgs inputEvent)
        {
            //Check each form, starting with the foremost, to see if they handled the event or not
            int handleIndex = -1;
            OldForm handleForm = null;

            for (int i = 0; i < forms.Count; i++)
            {
                //Only visible forms should be able to handle events!
                if (forms[i].Visible)
                {
                    handleForm = forms[i];
                    if (forms[i].HandleEvent(inputEvent))
                    {
                        handleIndex = i;
                        break;
                    }
                }
            }

                if (handleIndex >= 0)
                {
                    if (inputEvent is MouseEventArgs)
                    {
                        MouseEventArgs args = inputEvent as MouseEventArgs;
                        if (args.MouseEventType == MouseEventType.Click)
                        {
                            //We have to check both conditions - that handleIndex < count && the form
                            //we are moving is the same as the form that handled the event, because
                            //it is possible the event made us change game screens, and the form collection
                            //may have been modified in that case
                            if (handleIndex < forms.Count)
                            {
                                OldForm f = forms[handleIndex];
                                if (f == handleForm)
                                {
                                    forms.RemoveAt(handleIndex);
                                    forms.Insert(0, f);
                                    
                                }
                            }
                        }
                    }
                    return true;
                }
                return false;
                
            

       }

    }
}
