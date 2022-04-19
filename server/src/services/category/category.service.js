const { Category } = require('../../../models/Category.js');

const mongodbConnectionManager = require('../../../config/db.config.js');

let mockyData = [
  {
    name: 'Home',
    image: 'https://www.raneen.com/media/wysiwyg/Home-Appliances-AR_2.jpg',
    productList: [],
  },
  {
    name: 'Kitchen',
    image: 'https://www.raneen.com/media/wysiwyg/Kitchen-Appliances-AR_2.jpg',
    productList: [],
  },
  {
    name: 'Electronics',
    image: 'https://www.raneen.com/media/wysiwyg/TVs-_-Accessories--AR_1.jpg',
    productList: [],
  },
  {
    name: 'Mobiles',
    image:
      'https://www.raneen.com/media/wysiwyg/Mobiles-_-Accessories-AR_1.jpg',
    productList: [],
  },
  {
    name: 'Bathroom',
    image:
      'https://www.raneen.com/media/wysiwyg/Mobiles-_-Accessories-AR_1.jpg',
    productList: [],
  },
];

module.exports.seedCategoriesService = async function () {
  return new Promise(async (resolve, reject) => {
    if (mongodbConnectionManager.connected) {
      try {
        mockyData.forEach(async mocky => {
          await Category.create(mocky);
        });
        resolve('mocked');
      } catch (error) {
        reject(error);
      }
    } else {
      reject(error);
    }
  });
};

module.exports.getAllCategories = async function () {
  return new Promise(async (resolve, reject) => {
    if (mongodbConnectionManager.connected) {
      try {
        const data = await Category.find();
        resolve(data);
      } catch (error) {
        reject(error);
      }
    } else {
      reject(error);
    }
  });
};
