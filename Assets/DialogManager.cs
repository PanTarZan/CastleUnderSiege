using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DialogManager
{
    public void CreateXmlTemplate()
    {
        //do stworzenia pierwszej xmlki tylko

        var Template = new XmlDocument();
        XmlNode rootNode = Template.CreateElement("Entries");
        Template.AppendChild(rootNode);

        XmlNode entryNode = Template.CreateElement("Entry");
        rootNode.AppendChild(entryNode);

        XmlNode characterName = Template.CreateElement("Name");
        characterName.InnerText = "Zlodupiec";

        XmlNode characterSpeech = Template.CreateElement("Text");
        characterSpeech.InnerText = "Ty Psi ogonie!";

        XmlNode characterGraphic = Template.CreateElement("GraphicName");
        characterGraphic.InnerText = "Tex_lich";

        XmlNode characterBorderColor = Template.CreateElement("BorderColor");
        characterBorderColor.InnerText = "FF00D7";

        XmlNode characterScreenPosition = Template.CreateElement("ScreenPosition");
        characterScreenPosition.InnerText = "Left";

        entryNode.AppendChild(characterName);
        entryNode.AppendChild(characterSpeech);
        entryNode.AppendChild(characterGraphic);
        entryNode.AppendChild(characterBorderColor);
        entryNode.AppendChild(characterScreenPosition);


        Template.Save("test-doc.xml");
    }

    public List<DialogObject> LoadDialogXML(string dialogName)
    {
        List<DialogObject> listOfEntries = new List<DialogObject>();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("test-doc.xml");
        XmlNodeList entryNodes = xmlDoc.SelectNodes("//Entries/Entry");
        

        foreach (XmlNode node in entryNodes)
        {
            DialogObject DObject = new DialogObject();
            
                DObject.Name = node.SelectSingleNode("Name").InnerText;
                DObject.Text = node.SelectSingleNode("Text").InnerText;
                DObject.Graphic = node.SelectSingleNode("GraphicName").InnerText;
                DObject.BorderColor = node.SelectSingleNode("BorderColor").InnerText;
                DObject.ScreenPosition = node.SelectSingleNode("ScreenPosition").InnerText;
            
            listOfEntries.Add(DObject);
        }

        return listOfEntries;
    }
}

