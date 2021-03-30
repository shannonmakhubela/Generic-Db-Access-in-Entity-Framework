using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenDemo
{
    class service<T> where T : class 
    {
        schoolDemodbEntities context = new schoolDemodbEntities();
      
        //add Item
        public bool AddItem(T Item)
        {
            try
            {
                context.Set<T>().Add(Item);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }

        }

        //Delete Item.
        public bool DeleteItem(T Item)
        {
            try
            {
                context.Set<T>().Remove(Item);
                context.SaveChanges();

                return true;

            }catch(Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }

        // Update Item.
        public bool UpdateItem(T up)
        {
            try
            {
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                return false;
            }
        }
        // fetch single Item.
        public T FetchItem(Func<T, bool> id)
        {
            //query 
            try
            {
                var item = (from I in context.Set<T>() select I).FirstOrDefault(id);
                if (item != null)
                {
                    return item; ;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                ex.GetBaseException();
                return null ;
            }

        }
        // List of Items
        public List<T> ListItems()
        {
            try
            {
                //return a query.
                var Items = (from I in context.Set<T>() select I);
                List<T> ret = new List<T>();

                foreach (var item in Items)
                {
                    ret.Add(item);
                }
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
