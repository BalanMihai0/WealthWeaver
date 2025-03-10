import React from "react";
import AccountSelector from "../components/AccountSelector";
import Header from "../components/Header";
import { Link } from "react-router-dom";
import accounts from "../mock-data/bankAccounts.json";
const allAccounts = [...accounts.bankAccounts, ...accounts.creditCards];

/**
 * This page provides a form to initiate a money transfer between
 * two accounts
 *
 */
export default function TransferPage() {
  return (
    <main className="px-4 py-8">
      <Header active={"transfer"} />
      <div className="flex flex-col justify-center items-center">
        <div className="flex flex-col items-center self-center w-full md:w-1/2">
          <h1 className="text-gray-500 mb-4">TRANSFER MONEY</h1>
          <div className="bg-white shadow-lg rounded-lg p-8 mb-8 w-full dark:bg-gray-700 dark:text-white">
            <div className="py-8 flex flex-col gap-8">
              <div className="flex items-center flex-col gap-4 lg:flex-row">
                <p className="flex-1">
                  From Account <span className="text-red-500 mb-4">*</span>
                </p>
                <AccountSelector accounts={allAccounts} />
              </div>
              <div className="flex items-center flex-col gap-4 lg:flex-row">
                <p className="flex-1">
                  To Account <span className="text-red-500 mb-4">*</span>
                </p>
                <div className="flex flex-col items-center gap-2">
                  <AccountSelector accounts={allAccounts} selectedId={"3"} />
                  <p className="text-blue-500 text-sm cursor-pointer hover:underline">
                    Add New Account
                  </p>
                </div>
              </div>
              <div className="flex items-center flex-col gap-4 lg:flex-row">
                <label className="flex-1" htmlFor="amount">
                  Amount <span className="text-red-500 mb-4">*</span>
                </label>
                <input
                  type="number"
                  id="amount"
                  defaultValue="0.00"
                  className="py-2 px-4 bg-gray-200 rounded-full text-center dark:bg-gray-800 w-full md:w-auto"
                />
              </div>
              <div className="flex items-center flex-col gap-4 lg:flex-row">
                <label className="flex-1" htmlFor="memo">
                  Memo
                </label>
                <input
                  type="text"
                  id="memo"
                  placeholder="description about this transfer"
                  className="py-2 px-4 bg-gray-200 rounded-full text-center dark:bg-gray-800 w-full md:w-auto"
                />
              </div>
              <div className="flex justify-center mt-8">
                <Link
                  to="/transfer-success"
                  className="bg-blue-500 px-4 py-2 rounded-full text-white flex-1 text-center"
                >
                  PROCEED
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
