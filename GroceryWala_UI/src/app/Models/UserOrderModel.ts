export class UserOrderModel{
    orderId?:number;
    totalMRP: number;
    discountOnMRP: number;
    finalAmount: number;
    createdAt?: Date;
    couponCode?: string;
    extraDiscount?:number;
    userId?:string;

    constructor(totalMRP: number, discountOnMrp: number, finalAmount: number, 
        couponCode?: string, createdAt?: Date, id?:number, extraDiscount?:number){
        this.totalMRP = totalMRP;
        this.finalAmount = finalAmount;
        this.discountOnMRP = discountOnMrp;
        this.couponCode = couponCode;
        this.orderId = id;
        this.extraDiscount = extraDiscount;
        this.createdAt = createdAt;   
    }
}