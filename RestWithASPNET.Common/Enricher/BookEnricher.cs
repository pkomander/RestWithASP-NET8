using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.DTO.BookDto;
using RestWithASPNET.Hypermedia.Constants;
using System.Text;

namespace RestWithASPNET.Hypermedia.Enricher
{
    //Criando Biblioteca Common para evitar erro de referencia cruzada entre RestWithASPNET.DTO e RestWithASPNET.Hypermedia
    public class BookEnricher : ContentResponseEnricher<ReadBookDto>
    {
        protected override Task EnrichModel(ReadBookDto content, IUrlHelper urlHelper)
        {
            var path = "api/book";
            string link = GetLink(content.Id, urlHelper, path);

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPut
            });

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultDelete
            });

            return Task.CompletedTask;
        }

        private string GetLink(int id, IUrlHelper urlHelper, string path)
        {
            lock (this)
            {
                var url = new
                {
                    controller = path,
                    Id = id
                };
                var stringBuilder =  new StringBuilder(urlHelper.Link("DefaultApi", url));
                
                return stringBuilder.Replace("%2F", "/").ToString();
            }
        }
    }
}
