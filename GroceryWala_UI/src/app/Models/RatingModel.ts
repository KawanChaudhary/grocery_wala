export class RatingModel{
    id?:string;
    userId:string;
    productId:string;
    rating:number = 0;
    constructor(userId:string, productId:string, rating:number, id?:string) {
        this.userId = userId;
        this.productId = productId;
        this.rating = rating;
        this.id = id;
    }
}