//@description : login service for the user

const bcrypt = require('bcrypt');

const jsonwebtoken = require('jsonwebtoken');

const Customer = require('../../../models/Customer.js');

const { appLoginSchema } = require('../../utils/dataValidator.util.js');

module.exports.loginService = async function (email, password) {
  return new Promise(async (resolve, reject) => {
    try {
      //validation part
      const { value, error } = appLoginSchema.validate({
        email,
        password,
      });

      if (error) {
        throw `${error}`;
      }

      //simple check for admin
      if (email === 'admin@admin.com' && password === 'admin') {
        let adminToken = jsonwebtoken.sign(
          JSON.stringify({
            name: 'ITIStudentsAppAdmin',
            email: 'admin@admin.com',
          }),
          process.env.ADMIN_SECRET
        );
        resolve(adminToken);
      }

      let user = await Customer.findOne({ email: email });

      if (user) {
        //if user is registered before .

        const isPasswordValid = await bcrypt.compare(password, user.password);

        if (isPasswordValid) {
          const jwt = jsonwebtoken.sign(
            JSON.stringify(user),
            process.env.CLIENT_SECRET
          );
          resolve(jwt);
        } else {
          reject({
            message: 'invalid email or password',
          });
        }
      } else {
        reject({
          message: 'invalid email or password',
        });
      }
    } catch (error) {
      reject({
        message: error,
      });
    }
  });
};

//i need to take user profile in case of Oauth login style
