using BuildEventer.ViewModels;

namespace BuildEventer.Models
{
    public abstract class IAction : ViewModelBase
    {
        public abstract string Name { get; set; }
        public abstract string Type { get; set; }
        //public ICollection<string> Sources { get; set; }
        //public ICollection<string> Destinations { get; set; }

        //private ICollection<string> m_Sources;
        //public ICollection<string> Sources
        //{
        //    get
        //    {
        //        return m_Sources;
        //    }
        //    set
        //    {
        //        if (value != m_Sources)
        //        {
        //            m_Sources = value;
        //            OnPropertyChanged("Sources");
        //        }
        //    }
        //}

        //private ICollection<string> m_Destinations;
        //public ICollection<string> Destinations
        //{
        //    get
        //    {
        //        return m_Destinations;
        //    }
        //    set
        //    {
        //        if (value != m_Destinations)
        //        {
        //            m_Destinations = value;
        //            OnPropertyChanged("Destinations");
        //        }
        //    }
        //}
    }
}
