using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Misc.Manufacturer.Models;

namespace Nop.Plugin.Misc.Manufacturer.Helper
{
    public class ManufacturerHelper
    {
        

        public static List<char> GetAllCustomGroupsName()
        {
            //var list = new List<char>();
            //var listModel = GetAllGroups();
            //foreach(var item in listModel)
            //{
            //    list.Add(item.GroupName);
            //}
            //return list;
            var groupNameList = new List<char>();
            for (var i = 'A'; i <= 'Z'; i++)
            {
               groupNameList.Add(i);
            }
            return groupNameList;
        }

        //public static IList<ManufacturerGroupModel> GetAllGroups()
        //{
        //    var listModel = new List<ManufacturerGroupModel>();
        //    for (var i = 'A'; i <= 'Z'; i++)
        //    {
        //        listModel.Add(new ManufacturerGroupModel()
        //        {
        //            GroupId = (i - 'A') + 1,
        //            GroupName = i
        //        });
        //    }
        //    return listModel;
        //}

        public static IList<char> GetAvailableGroupsName(List<ManufacturerModel> grouplist)
        {
            var list = new List<char>();
            foreach (var item in grouplist)
            {
                list.Add(item.GroupName);
            }
            return list;
        }

        public static List<int> GetAllGroupIds()
        {
            var groupIdList = new List<int>();
            for (var i = 'A'; i <= 'Z'; i++)
            {
                groupIdList.Add((i - 'A') + 1);
               
            }
            return groupIdList;
        }
    }
}
