namespace IDOBusTech.NETTech.Test.Mapper
{
    using System.Collections.Generic;
    using IDOBusTech.NETTech.Test.Model;
    using IDOBusTech.NETTech.Test.ViewModel;
    using System.Linq;

    public static class Map
    {
        public static OwnerModel From(Owner owner)
        {
            var ownerModel = new OwnerModel
            {               
                ReposUrl = owner.repos_url,
                UserName = owner.login,
                Location = owner.url,
                AvatarUrl = owner.avatar_url,
            };

            return ownerModel;
        }

        public static List<RepoModel> From(List<Repo> repos)
        {
           var repoModel = new List<RepoModel>();

           foreach(var repo in repos)
           {
              repoModel.Add(new RepoModel { Id = repo.id, Name = repo.name, FullName = repo.full_name, Stargazers = repo.stargazers_count });
           }

           return repoModel.OrderByDescending(o=>o.Stargazers).Take(5).ToList();
        }
    }
}