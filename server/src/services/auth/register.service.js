//@description : this service will take user data and give him back a token

const bcrypt = require('bcrypt');

const { appRegisterSchema } = require('../../utils/dataValidator.util.js');

const Customer = require('../../../models/Customer.js');

const jsonwebtoken = require('jsonwebtoken');

module.exports.registerService = async function (
  firstName,
  lastName,
  email,
  password,
  phone,
  company
) {
  //validating user Schema
  return new Promise(async (resolve, reject) => {
    try {
      //   validation part
      const { value, error } = appRegisterSchema.validate({
        firstName,
        lastName,
        password,
        email,
        phone,
        company,
      });

      if (error) {
        throw `${error}`;
      }
      //if no errors then lets go

      //check if the email is registered before

      let user = await Customer.findOne({
        email: email,
      });

      if (user) {
        reject({
          message: new Error('email is registered before').message,
        });
      } else {
        //if there's no user with that email registered before .

        //we need to fill the customer collection with the user data

        //decrypt user password first

        const salt = await bcrypt.genSalt(15);

        const hashedPassword = await bcrypt.hash(password, salt);

        user = await Customer.create({
          firstName: firstName,
          lastName: lastName,
          phone: phone,
          email: email,
          password: hashedPassword,
          company: company ?? '',
        });

        console.log(user);
        //if the user is ok ..
        //then we wanna resolve a token for the user

        let jwt;
        try {
          jwt = jsonwebtoken.sign(
            JSON.stringify(user),
            process.env.CLIENT_SECRET
          );
        } catch (error) {
          throw 'cannot generate user token';
        }

        //if no error , generate the user token

        resolve(jwt);
      }
    } catch (error) {
      reject({
        message: error,
      });
    }
  });
};
