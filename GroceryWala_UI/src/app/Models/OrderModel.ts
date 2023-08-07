export class OrderModel {
    userId: string;
    orderId: number;
    name: string;
    productId: number;
    productQuantity: number;
    price: number;
    discount: number;
    imageAddress?: string;
    id?: string;

    constructor(orderId: number, userId: string, name:string, productId: number, productQuantity: number, price: number,
        discount: number,  id?: string, imageAddress?: string) {
        
            this.orderId = orderId;
            this.userId = userId;
            this.name = name;
            this.productId = productId;
            this.productQuantity = productQuantity;
            this.price = price;
            this.discount = discount;
            this.id = id;
            this.imageAddress = imageAddress;

    }

}