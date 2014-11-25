using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using MWDataSerilizationType;

namespace MWDataEntity
{
    class MWDataEntityImpl : IMWDataEntityInterface
    {
        public void Dispose()
        {

        }

        public IList<Apparel> SearchByTagsAndValue(
            SearchingArgs args, 
            SearchingArgs notContainArgs, 
            string[] orderBy,
            string[] orderByDescending,
            int currentPage,
            int pageCount = 200)
        {
            using (var db = new MWDBEntities())
            {
                var parmer = Expression.Parameter(typeof(MW_SEARCHABLE_ITEM), "Alfred_Search");

                Expression td= MWLambdaGeneration.generateWhereLambda(parmer, args.args,true);
                Expression ntd = MWLambdaGeneration.generateWhereLambda_Not(parmer, notContainArgs.args);

                Expression searchingQuery = null;
                if (td != null)
                    searchingQuery = td;

                if (ntd != null )
                {
                    if(td != null)
                        searchingQuery = Expression.AndAlso(searchingQuery, ntd);
                    else
                        searchingQuery = ntd;
                }
 
                IList<Apparel> reVal = new List<Apparel>() as IList<Apparel>;
                if (searchingQuery == null)
                    return reVal;

                var lambda = Expression.Lambda<Func<MW_SEARCHABLE_ITEM, bool>>
                    (searchingQuery, new ParameterExpression[] { parmer });

                IQueryable<MW_SEARCHABLE_ITEM> queryableData = db.MW_SEARCHABLE_ITEM.AsQueryable<MW_SEARCHABLE_ITEM>();

                MethodCallExpression whereCallExpression = Expression.Call(
                     typeof(Queryable),
                     "Where",
                     new Type[] { queryableData.ElementType },
                     queryableData.Expression,
                     lambda);

                if (orderBy != null && orderBy.Count() != 0)
                {
                    whereCallExpression = 
                        expendExpressionWithOrderParmer(parmer, orderBy, whereCallExpression, false);
                }

                if (orderByDescending != null && orderByDescending.Count() != 0)
                {
                    whereCallExpression =
                        expendExpressionWithOrderParmer(parmer, orderByDescending, whereCallExpression, true);
                }

                var result = queryableData.Provider
                             .CreateQuery<MW_SEARCHABLE_ITEM>(whereCallExpression).Skip(currentPage * pageCount).Take(pageCount);

     
                foreach (MW_SEARCHABLE_ITEM p in result)
                {
   
                    if(p.MW_UNIQ_ITEM.Count>=1)
                    {
                        Apparel c = MWSerilizationTypeFactory.CreateCloth(p);
                        reVal.Add(c);
                    }
                   
                }
                return reVal;
            }
        }

        internal class MWSearchableItemComparer : EqualityComparer<MW_SEARCHABLE_ITEM>
        {
            public override bool Equals(MW_SEARCHABLE_ITEM x, MW_SEARCHABLE_ITEM y)
            {
                return x.abstract_item_id == y.abstract_item_id;
            }

            public override int GetHashCode(MW_SEARCHABLE_ITEM obj)
            {
                return obj == null ? 0 : obj.abstract_item_id;
            }
        }

        public MethodCallExpression expendExpressionWithOrderParmer(
            ParameterExpression parmer, 
            string[] orderBy, 
            MethodCallExpression whereExpression,
            Boolean descending)
        {
            MethodCallExpression reVal = whereExpression;
            for (int index = 0; index < orderBy.Length; ++index)
            {
                Expression orderQuery = MWLambdaGeneration.generateOrderByLambda(parmer, orderBy[index]);
                Expression orderLambda = null;
                Type compairType = null;
                String str_order_function_name = null;
                if (index == 0)
                    str_order_function_name = "OrderBy";
                else
                    str_order_function_name = "ThenBy";

                if (descending)
                    str_order_function_name += "Descending";

                if (orderBy[index].ToLower() == "price" || orderBy[index].ToLower() == "hot")
                {
                    orderLambda = Expression.Lambda<Func<MW_SEARCHABLE_ITEM, float>>
                        (orderQuery, new ParameterExpression[] { parmer });
                    compairType = typeof(float);
                }
                else
                {
                    orderLambda = Expression.Lambda<Func<MW_SEARCHABLE_ITEM, string>>
                        (orderQuery, new ParameterExpression[] { parmer });
                    compairType = typeof(string);
                }

                reVal = Expression.Call(
                    typeof(Queryable),
                    str_order_function_name,
                    new Type[] { typeof(MW_SEARCHABLE_ITEM), compairType },
                    reVal,
                    orderLambda);
            }

            return reVal;
        }

        public IList<string> QueryProsibleDataInTag(string tags)
        {
            try
            {
                Type theMathType = Type.GetType("DOAEntity.DOAEntityService");

                // parameter types
                Type[] paramTypes = Type.EmptyTypes;

                // method
                MethodInfo method = theMathType.GetMethod("QueryProsibleDataIn" + tags, paramTypes);

                return method.Invoke(this, null) as IList<string>;
            }
            catch (Exception)
            {
                Console.WriteLine("no such a tag!");
            }

            return null;
        }
        
        public IList<string> QueryProsibleDataInBrand()
        {
            using (var db = new MWDBEntities())
            {
                var query = (from s in db.MW_BRAND
                            select s.brand_name).Distinct();
                
                return query.ToList<string>() as IList<string>;                
            }
        }
        
        public IList<string> QueryProsibleDataInColor()
        {
            using (var db = new MWDBEntities())
            {
                var query = (from s in db.MW_SEARCHABLE_ITEM
                                 select s.MW_COLOUR.colour_name).Distinct();
                return query.ToList<string>() as IList<string>;                
            }
        }

        public IList<string> QueryProsibleDataInGender()
        {
            using (var db = new MWDBEntities())
            {
                var query = (from s in db.MW_SEARCHABLE_ITEM
                            select s.MW_ABSTRACT_ITEM.gender).Distinct();

                return query.ToList<string>() as IList<string>;                
            }
        }
        
       public IList<string> QueryProsibleDataInSize()
       {
            //using (var db = new MWDBEntities())
            //{
            //    var query = (from s in db.MW_ACTIVE_ITEM
            //                select s.size_val).Distinct();

            //    return query.ToList<string>() as IList<string>;                
            //}
           return null;
       }
       //public string SearchingByURL(string picURL)
       //{
       //    using (var db = new MWDBEntities())
       //    {
       //        var result = from searchable_item in db.MW_SEARCHABLE_ITEM
       //                     where searchable_item.MW_PICTURE.url.Equals(picURL)
       //                     select searchable_item;
       //        return result.ToString();
       //    }
       //}

       public Boolean UpdateLikeCountDB(string searchableId , string userId)
       {
           using (var db = new MWDBEntities())
           {
               int nID = int.Parse(searchableId);
               var cloth = (from item in db.MW_SEARCHABLE_ITEM
                            where item.searchable_item_id == nID
                            select item).FirstOrDefault();

               var user_cpig = (from u in db.MW_SEARCHABLE_ITEM
                                from a in u.MW_USER
                                where a.user_id == userId && u.searchable_item_id == nID
                                select a).FirstOrDefault();

               if (user_cpig == null)
               {
                   user_cpig = (from u in db.MW_USER
                                where u.user_id == userId
                                select u).FirstOrDefault();

                   cloth.MW_USER.Add(user_cpig);
                   db.SaveChanges();
                   return true;
               }
               else
                   return false;
           }
       }

       public int getLikeCount(string item_id)
       {
           using (var db = new MWDBEntities())
           {
               int nID = int.Parse(item_id);
               var cloth = (from item in db.MW_SEARCHABLE_ITEM
                            where item.searchable_item_id.Equals(nID)
                            select item).FirstOrDefault();
               return cloth.MW_USER.Count;
           }
       }
 
    }
}
