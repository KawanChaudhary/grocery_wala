export class CommentModel{

    userId: string;
    firstName: string;
    productId:string
    text:string
    time?:Date;
    id?: string;
    lastName?: string;

    constructor(userId: string, firstName: string, productId: string, text:string,  time?: Date, lastName?: string, id?: string,){
        this.userId = userId;
        this.text = text
        this.firstName = firstName
        this.productId = productId
        this.time = time
        this.id = id
        this.lastName = lastName

    }

}