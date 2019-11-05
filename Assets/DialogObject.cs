using System.Collections;
using System.Collections.Generic;

public class DialogObject
{
    private string name;
    private string text;
    private string graphic;
    private string borderColor;
    private string screenPosition;
    
    public string Name { get => name; set => name = value; }
    public string Text { get => text; set => text = value; }
    public string Graphic { get => graphic; set => graphic = value; }
    public string BorderColor { get => borderColor; set => borderColor = value; }
    public string ScreenPosition { get => screenPosition; set => screenPosition = value; }
}
