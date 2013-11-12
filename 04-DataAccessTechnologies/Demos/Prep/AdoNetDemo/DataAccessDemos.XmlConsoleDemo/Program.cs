using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataAccessDemos.XmlConsoleDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      //CreateXml();
      //WalkDOM();

      CreateXmlWithLinq();
      ReadXmlWithLinq();
    }

    private static void CreateXmlWithLinq()
    {
      XDocument doc = new XDocument(
        new XElement("Positions",
          CreatePositionXElement(1,1,DateTime.Now.AddYears(-1),35,-112),
          CreatePositionXElement(2,1,DateTime.Now.AddYears(-1),36,-113),
          CreatePositionXElement(3,1,DateTime.Now.AddYears(-1),37,-114),
          CreatePositionXElement(4,2,DateTime.Now,38,-115),
          CreatePositionXElement(5,2,DateTime.Now,39,-116)
          )
        );

      doc.Save("Positions linq.xml");
    }


    private static void ReadXmlWithLinq()
    {
      XDocument doc = XDocument.Load("Positions linq.xml");
      XElement root = doc.Root;
      var lats = from e in root.Elements("Position")
                 select new
                 {
                   PositionID = (int?)e.Attribute("PositionID"),
                   Latitude = (int?)e.Element("Latitude"),
                   Longitude = (int?)e.Element("Longitude"),
                 };
    }


    private static XElement CreatePositionXElement(int PositionID, int CruiseID, DateTime ReportedAt, float Latitude, float Longitude)
    {
      return new XElement("Position",
              new XAttribute("PositionID", PositionID),
              new XAttribute("CruiseID", CruiseID),
              new XElement("ReportedAt", ReportedAt),
              new XElement("Latitude", Latitude),
              new XElement("Longitude", Longitude)
              );
    }

    private static void CreateXml()
    {
      XmlDocument doc = new XmlDocument();
      XmlElement root = doc.CreateElement("Positions");
      doc.AppendChild(doc.CreateComment("This document was created in code! W00t!"));
      doc.AppendChild(root);

      root.AppendChild(CreatePositionElement(doc, 1, 1, DateTime.Now.AddYears(-1), 35, -112));
      root.AppendChild(CreatePositionElement(doc, 2, 1, DateTime.Now.AddYears(-1), 36, -113));
      root.AppendChild(CreatePositionElement(doc, 3, 1, DateTime.Now.AddYears(-1), 37, -114));
      root.AppendChild(CreatePositionElement(doc, 4, 2, DateTime.Now, 38, -115));
      root.AppendChild(CreatePositionElement(doc, 5, 2, DateTime.Now, 39, -116));

      doc.Save("Positions Programmatic.xml");
    }

    private static XmlNode CreatePositionElement(XmlDocument Document, int PositionID, int CruiseID, DateTime ReportedAt, float Latitude, float Longitude)
    {
      XmlElement positionElement = Document.CreateElement("Position");

      XmlAttribute positionId = Document.CreateAttribute("PositionID");
      positionId.Value = PositionID.ToString();
      positionElement.Attributes.Append(positionId);

      XmlAttribute cruiseId = Document.CreateAttribute("CruiseID");
      cruiseId.Value = CruiseID.ToString();
      positionElement.Attributes.Append(cruiseId);

      XmlElement reportedAt = Document.CreateElement("ReportedAt");
      reportedAt.InnerText = ReportedAt.ToString();
      positionElement.AppendChild(reportedAt);

      XmlElement latitude = Document.CreateElement("Latitude");
      latitude.InnerText = Latitude.ToString();
      positionElement.AppendChild(latitude);
      
      XmlElement longitude = Document.CreateElement("Longitude");
      longitude.InnerText = Longitude.ToString();
      positionElement.AppendChild(longitude);

      return positionElement;

    }

    private static void WalkDOM()
    {
      XmlDocument doc = new XmlDocument();
      doc.Load("Positions Programmatic.xml");
      var root = doc.DocumentElement;

      RecurseNode(root, 0);
    }

    static void RecurseNode(XmlNode Node, int Level)
    {
      string name = (string.IsNullOrWhiteSpace(Node.Name)) ? "" : Node.Name;
      if (Node.NodeType == XmlNodeType.Text)
        Console.WriteLine("{0}{1}", new string(' ', Level), Node.Value);
      else
      {
        Console.WriteLine("{0}{1}", new string(' ', Level), Node.Name);
        if (Node.Attributes != null)
          foreach (XmlAttribute attr in Node.Attributes)
            Console.WriteLine("{0}{1}={2}", new string(' ', Level + 2), attr.Name, attr.Value);
        foreach (XmlNode child in Node.ChildNodes)
          RecurseNode(child, Level + 1);
      }
    }
  }
}
