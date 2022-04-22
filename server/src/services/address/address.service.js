//TODO after salah and pray then we will do the following
//add the address and tie it with the user by simply use the auth in order to complete the process

const { request } = require('express');

const mongooseDBConnectionManager = require('../../../config/db.config.js');

const Address = require('../../../models/Address.js');

const { addressValidator } = require('../../utils/dataValidator.util.js');

/**
 *
 * @param {request} req
 * @returns {Promise<any>}
 */
module.exports.attachAddressToClient = async function (req) {
  return new Promise(async (resolve, reject) => {
    try {
      const {
        address1,
        address2,
        city,
        country,
        countryCode,
        province,
        province_code,
        zip,
      } = req.body;

      const { value, error } = addressValidator.validate({
        address1,
        address2,
        city,
        country,
        countryCode,
        province,
        province_code,
        zip,
      });

      if (error) {
        throw error;
      }

      if (req.user) {
        //^if there's user and the data coming from the body is loaded with address correct then we can go and complete the request
        let customerId = req.user._id;
        const userAddress = await Address.create({
          address1,
          address2,
          city,
          country,
          countryCode,
          customerId,
          province,
          province_code,
          zip,
        });
        //if all re ok then
        req.user.address = userAddress;
        resolve(req.user);
      } else {
        reject({
          message: 'something wrong with the authentication process ..',
        });
      }
    } catch (error) {
      reject(error);
    }
  });
};
