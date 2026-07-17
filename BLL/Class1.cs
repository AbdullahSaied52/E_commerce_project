using class_1_DTO;
using dal_4;
namespace BLL
{
    public class ClsBLL
    {
        public static void insert(product_dto p)
        {
            dal.insert(p);
        }

        public static product_dto info(string name)
        {
            return dal.list_info(name);
        }

        public static int sell(string name,int amount)
        {
            return dal.sell(name, amount);
        }

    }
}
