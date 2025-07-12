// recommendationEngine.js

function recommend(items) {
    // খুব সহজে একটি রিকমেন্ডেশন লিস্ট বানানো হলো
    return items.filter(item => item.rating > 4);
}

// সেম্পল ডাটা
const products = [
    {name: 'Product 1', rating: 3},
    {name: 'Product 2', rating: 5},
    {name: 'Product 3', rating: 4.5},
];

// রিকমেন্ডেশন দেখাও
console.log('Recommended products:', recommend(products));
