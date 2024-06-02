using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.classes
{
    public class ElementsList
    {
        private List<Element> elementList = new List<Element>();

        public void AddElement(Element element)
        {
            elementList.Add(element);
        }

        public void RemoveElement(Element element)
        {
            elementList.Remove(element);
        }

        public List<Element> GetElements()
        {
            return elementList;
        }
    }
}
