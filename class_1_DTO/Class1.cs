namespace class_1_DTO
{
    public class product_dto
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public int price { get; set; }

        public product_dto(int id, string name, string type, int amount, int price)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.price = price;
            this.amount = amount;
        }
    }
}
