export class CartModel{
    id?: string;
    userId: string;
    productId: string;
    quantity: number;

    constructor(userId: string, productId: string, quantity: number, id?: string) {
        this.productId = productId;
        this.quantity = quantity;
        this.id = id;
        this.userId = userId;
    }
}