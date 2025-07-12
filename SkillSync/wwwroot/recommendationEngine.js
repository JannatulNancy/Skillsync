/**
 * Skill-based recommendation function
 * @param {Array} userSkills - ইউজারের স্কিল এর লিস্ট
 * @param {Array} items - প্রোডাক্ট বা জব লিস্ট, যাদের সাথে স্কিল ম্যাপ করা আছে
 * @returns {Array} - ইউজারের স্কিলের সাথে মিল থাকা আইটেম গুলো
 */
function recommendBySkills(userSkills, items) {
    return items.filter(item => {
        return item.skills.some(skill => userSkills.includes(skill));
    });
}

// স্যাম্পল ইউজার স্কিল
const userSkills = ['JavaScript', 'React', 'Node.js'];

// স্যাম্পল আইটেম লিস্ট
const items = [
    { id: 1, name: 'Frontend Developer Job', skills: ['HTML', 'CSS', 'JavaScript', 'React'] },
    { id: 2, name: 'Backend Developer Job', skills: ['Node.js', 'Express', 'MongoDB'] },
    { id: 3, name: 'Data Scientist Course', skills: ['Python', 'Machine Learning'] },
    { id: 4, name: 'Mobile App Developer Job', skills: ['Flutter', 'Dart'] },
    { id: 5, name: 'Fullstack Developer Job', skills: ['JavaScript', 'Node.js', 'React'] },
];

// রিকমেন্ডেশন দেখাও
const recommendedItems = recommendBySkills(userSkills, items);
console.log('Recommended for user based on skills:', recommendedItems);

