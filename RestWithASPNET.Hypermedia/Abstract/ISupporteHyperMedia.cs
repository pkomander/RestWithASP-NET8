using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Hypermedia.Abstract
{
    public interface ISupporteHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
