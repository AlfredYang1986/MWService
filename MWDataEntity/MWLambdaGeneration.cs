/************************************************************************/
/* Created by Alfred Yang                                               */
/* It is Magic, Do not touch                                            */
/************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;


namespace MWDataEntity
{
    class MWLambdaGeneration
    {
        public static Expression generateBrandLambda(ParameterExpression parmer)
        {
            var abstract_set = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty("MW_ABSTRACT_ITEM"));
            var brand_set = Expression.Property(abstract_set, typeof(MW_ABSTRACT_ITEM).GetProperty("MW_BRAND"));
            return Expression.Property(brand_set, typeof(MW_BRAND).GetProperty("brand_name"));
        }

        public static Expression generateColorLambda(ParameterExpression parmer)
        {
            var color_set = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty("MW_COLOUR"));
            return Expression.Property(color_set, typeof(MW_COLOUR).GetProperty("colour_name"));
        }
 
        public static Expression generateCategoryLambda(ParameterExpression parmer)
        {
            var abstract_set = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty("MW_ABSTRACT_ITEM"));
            var category_set = Expression.Property(abstract_set, typeof(MW_ABSTRACT_ITEM).GetProperty("MW_CATEGORY"));
            return Expression.Property(category_set, typeof(MW_CATEGORY).GetProperty("category_name"));
        }

        public static Expression generateStyleLambda(ParameterExpression parmer, IList<string> sence)
        {
            var inner_parmmer = Expression.Parameter(typeof(MW_TAG), "Catherine");
            var generateLambdaExp = MWExpressionManyToManyHelper.innerLambdaGen<MW_TAG, string ,string>(
                                    inner_parmmer, sence,  "tag_name", null, null,"sence");

            Expression user_tag_item_set = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty("MW_TAG"));
            Type user_tag_item_set_type = MWExpressionManyToManyHelper.GetIEnumerableImpl(user_tag_item_set.Type);
            user_tag_item_set = Expression.Convert(user_tag_item_set, user_tag_item_set_type);
            Type genericArgumentType = user_tag_item_set_type.GetGenericArguments()[0];
            Type predictionType = typeof(Func<,>).MakeGenericType(genericArgumentType, typeof(bool));

            MethodInfo anyMethod = (MethodInfo)MWExpressionManyToManyHelper.GetGenericMethod(
                                  typeof(Enumerable),
                                  "Any",
                                  new[] { genericArgumentType },
                                  new[] { user_tag_item_set_type, predictionType },
                                  BindingFlags.Static);

            return Expression.Call(anyMethod, user_tag_item_set, generateLambdaExp);
        }

        public static Expression generateSizeLambda(ParameterExpression parmer, IList<string> sizes)
        {
            var inner_parmer = Expression.Parameter(typeof(MW_UNIQ_ITEM), "Catherine");
            var generateLambdaExp = MWExpressionManyToManyHelper.innerLambdaGen<MW_UNIQ_ITEM, MW_SIZE, string>(
                                      inner_parmer, sizes, "MW_SIZE", "size_val", null, "size");

            Expression item_size_set = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty("MW_UNIQ_ITEM"));
            Type item_size_set_type = MWExpressionManyToManyHelper.GetIEnumerableImpl(item_size_set.Type);
            item_size_set = Expression.Convert(item_size_set, item_size_set_type);
            Type genericArgumentType = item_size_set_type.GetGenericArguments()[0];
            Type predictionType = typeof(Func<,>).MakeGenericType(genericArgumentType, typeof(bool));

            MethodInfo anyMethod = (MethodInfo)MWExpressionManyToManyHelper.GetGenericMethod(
                                  typeof(Enumerable),
                                  "Any",
                                  new[] { genericArgumentType },
                                  new[] { item_size_set_type, predictionType },
                                  BindingFlags.Static);

            return Expression.Call(anyMethod, item_size_set, generateLambdaExp);

          
        }

        public static Expression generatePriceLambda(ParameterExpression parmer, float lessNumber, float greaterNumber)
        {
            var price_set = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty("price"));

            var geaterThanExp = Expression.GreaterThanOrEqual(price_set, Expression.Constant(lessNumber, typeof(float)));
            var lessThanExp = Expression.LessThanOrEqual(price_set, Expression.Constant(greaterNumber, typeof(float)));
           
            return Expression.AndAlso(lessThanExp, geaterThanExp);
        }

        public static Expression generateWhereLambda(ParameterExpression parmer,
                                                     IDictionary<string, IList<string> > searchArgs,
                                                     Boolean check)
        {
            if (searchArgs == null || searchArgs.Count == 0)
                return null;

            Expression exp_all_tags = null;
            foreach (var pair in searchArgs)
            {
                var tags = pair.Key;
                var valueList = pair.Value;
                Expression exp_tag = null;

                if (tags.ToLower() == @"discount")
                {
                    exp_tag = generateEqualLambda(parmer, @"discount", new Nullable<float>(float.Parse(pair.Value[0])));
                }
                else
                {
                    if (tags.ToLower() == @"category")
                    {
                        List<string> categoryList = new List<string>();

                        using (var db = new MWDataEntity.MWDBEntities())
                        {


                            foreach (var value in valueList)
                            {
                                //MW_Category and Category_relationship join together ------source_cat_relation_set
                                //source_cat_relation_set and MW_category join together ----target_cat_relation_set
                                //select from target_cat_relation_set
                                var query = from source_category_set in db.MW_CATEGORY
                                            join cat_relation_set in db.MW_CATEGORY_RELATIONSHIP
                                            on source_category_set.category_id equals cat_relation_set.source_category into source_cat_relation_set
                                            from source in source_cat_relation_set
                                            join target_category_set in db.MW_CATEGORY
                                            on source.target_category equals target_category_set.category_id into target_cat_ralation_set
                                            from last in target_cat_ralation_set
                                            where source_category_set.category_name.ToLower().Equals(value.ToLower()) && (source.relationship >= 0)
                                            select last.category_name;

                                foreach (var category_name in query)
                                {
                                    categoryList.Add(category_name.ToString());
                                }
                                categoryList.Add(value);
                            }

                        }
                        valueList = categoryList;
                    }

                    exp_tag = generatLambdaExp_tags(tags, valueList, parmer);
                }

                if (exp_all_tags == null)
                    exp_all_tags = exp_tag;
                else if(check)
                    exp_all_tags = Expression.AndAlso(exp_tag, exp_all_tags);
                else
                    exp_all_tags = Expression.OrElse(exp_tag, exp_all_tags);
            }

            return exp_all_tags;
        }

        public static Expression generatLambdaExp_tags(string tags, IList<string> valueList, ParameterExpression parmer)
        {
            Expression exp_tag = null;
            dynamic exp;
            if (tags.ToLower().Equals("size"))
            {
                exp = generateSizeLambda(parmer, valueList);
                exp_tag = exp;
            }
            else if (tags.ToLower().Equals("price"))
            {
                exp_tag = generatePriceLambda(parmer, float.Parse(valueList[0]), float.Parse(valueList[1]));
            }
            else if (tags.ToLower().Equals("sence"))
            {
                exp_tag = generateStyleLambda(parmer, valueList);
            }
            else
            {
                foreach (var value in valueList)
                {
                    exp = generateEqualLambda(parmer, tags, value);

                    if (exp_tag == null)
                        exp_tag = exp;
                    else
                        exp_tag = Expression.OrElse(exp, exp_tag);
                }
            }
           return exp_tag;
        }

        public static Expression generateWhereLambda_Not(ParameterExpression parmer,
                                                         IDictionary<string, IList<string>> searchArgs)
        {
            if (searchArgs == null || searchArgs.Count == 0)
                return null;
            return Expression.Not(MWLambdaGeneration.generateWhereLambda(parmer, searchArgs,false));
        }

        public static Expression generateOrderByLambda(ParameterExpression parmer, string strColum)
        {
            Expression orderby_lambda = null;
            if (strColum.ToLower() == "brand")
                orderby_lambda = MWLambdaGeneration.generateBrandLambda(parmer);
            else if (strColum.ToLower() == "category")
                orderby_lambda = MWLambdaGeneration.generateCategoryLambda(parmer);
            else
                orderby_lambda = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty(strColum.ToLower()));

            return orderby_lambda;
        }

        public static Expression generateEqualLambda(ParameterExpression parmer, string tag, string value)
        {
            var left = generateLeftLambda(parmer, tag);
            var right = Expression.Constant(value, typeof(string));
          
            return Expression.Equal(left, right);
        }

        public static Expression generateEqualLambda<T>(ParameterExpression parmer, string tag, T value)
        {
            var left = generateLeftLambda(parmer, tag);
            var right = Expression.Constant(value, typeof(T));

            try
            {
                return Expression.Equal(left, right);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static Expression generateContainLambda(ParameterExpression parmer, string tag, string value)
        {
            Expression left = null;
            
            left = generateLeftLambda(parmer, tag);
            var right = Expression.Constant(value.ToLower(), typeof(string));
      
            MethodInfo toLowerMethod, containsMethod;
            toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            left = Expression.Call(left, toLowerMethod);

            containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

            return Expression.Call(left, containsMethod, right);
        }

        private static Expression generateLeftLambda(ParameterExpression parmer, string tag)
        {
            Expression left = null;
            if (tag.ToLower() == @"brand")
                left = MWLambdaGeneration.generateBrandLambda(parmer);
            else if (tag.ToLower() == @"color")
                left = MWLambdaGeneration.generateColorLambda(parmer);
            else if (tag.ToLower() == @"category")
                left = MWLambdaGeneration.generateCategoryLambda(parmer);
            else
                left = Expression.Property(parmer, typeof(MW_SEARCHABLE_ITEM).GetProperty(tag.ToLower()));

            return left;
        }
    }
}
