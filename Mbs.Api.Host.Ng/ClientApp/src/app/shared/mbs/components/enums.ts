export class Enums {
    static compare(e1: any, e2: any): boolean {
        const a1 = e1.toString().toLowerCase();
        const a2 = e2.toString().toLowerCase();
        return a1 === a2;
    }
}
