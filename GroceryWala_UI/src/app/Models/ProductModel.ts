export class ProductModel {
    id?:number;
    name: string;
    price: number;
    description: string;
    quantity: number;
    category: number;
    stock: number;
    discount: number;
    sizeType: number;
    rating?: number;
    totalRatings?: number;
    otherDetails?: string;
    reviewCount?: number;
    isIncart?: boolean;
    
    constructor(name: string, price: number, description: string, quantity: number, 
      category: number, stock: number, discount: number, sizeType: number, otherDetails?: string, 
      id?: number, rating?: number, totalRatings?: number, reviewCount?: number, isIncart?: boolean) {
        
      this.id = id;
      this.name = name;
      this.price = price;
      this.description = description;
      this.quantity = quantity;
      this.category = category;
      this.stock = stock;
      this.discount = discount;
      this.sizeType = sizeType;
      this.otherDetails = otherDetails;
      this.rating = rating;
      this.totalRatings = totalRatings;
      this.reviewCount = reviewCount;
      this.isIncart = false;
    }

  }
  