const { request } = require('express');

const fs = require('fs');

const path = require('path');

const Product = require('../../../models/Product.js');

const category = require('../../../models/Category.js');

const mongodbConnectionManager = require('../../../config/db.config.js');

/**
 *
 * @param {request} reqObj
 * @returns
 */
module.exports.addProductsService = async function (reqObj) {
  return new Promise(async (resolve, reject) => {
    try {
      //reqObj it will get the files

      let product = await Product.create({
        title: reqObj.body.title,
        description: reqObj.body.description,
        vendor: reqObj.body.vendor,
        tags: reqObj.body.tags.split(','),
        weight: reqObj.body.weight,
        weight_unit: reqObj.body.weight_unit,
        price: reqObj.body.price,
        amount: reqObj.body.amount,
        category: reqObj.body.category,
      });

      //create folder 1
      let productFolderPath = path.join(
        __dirname,
        `../../public/${product._id}`
      );

      try {
        if (!fs.existsSync(productFolderPath)) {
          fs.mkdirSync(productFolderPath, { recursive: true });
        }
      } catch (folderError) {
        reject(folderError);
      }
      //loop through images and push them into the folder
      // console.log(reqObj.files);
      reqObj.files.images.forEach(img => {
        // console.log(img.buffer);
        let b64 = Buffer.from(img.buffer, 'base64');
        let imagePath = productFolderPath + '/' + img.originalname;
        // console.log(reqObj.body);
        product.images.push(
          reqObj.protocol +
            '://' +
            reqObj.headers.host +
            '/' +
            `${product._id}` +
            '/' +
            img.originalname
        );
        fs.writeFileSync(imagePath, b64);
      });

      //TODO push the the item into one of the system categories

      let targetCategory = await category.Category.findOne({
        name: product.category,
      });

      targetCategory.productList.push(product._id);

      await targetCategory.save();
      await product.save();
      resolve(product);
    } catch (error) {
      reject(error);
    }
  });
};

/**
 * @description : returns all the products stored in the database
 * @returns
 */
module.exports.getAllProductsService = async function () {
  return new Promise(async (resolve, reject) => {
    if (mongodbConnectionManager.connected) {
      try {
        const products = await Product.find();

        resolve(products);
      } catch (error) {
        reject(error);
      }
    } else {
      reject({
        message: 'db problems | connection | server down  !',
      });
    }
  });
};
