using AutoMapper;
using Backend.Domain.Entities;
using Backend.Models;

namespace Backend.ApplicationCore.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TypePropriete, TypeProprieteDto>().ReverseMap();
            CreateMap<TypePropriete, TypeProprieteResponse>().ReverseMap();

            CreateMap<Propriete, ProprieteDto>().ReverseMap();
            CreateMap<Propriete, ProprieteResponse>().ReverseMap();

            CreateMap<Produit, ProduitDto>().ReverseMap();
            CreateMap<Produit, ProduitResponse>().ReverseMap();

            CreateMap<Commande, CommandeDto>().ReverseMap();
            CreateMap<Commande, CommandeResponse>().ReverseMap();

            CreateMap<LigneCommande, LigneCommandeDto>().ReverseMap();
            CreateMap<LigneCommande, LigneCommandeResponse>().ReverseMap();

        }

    }
}
