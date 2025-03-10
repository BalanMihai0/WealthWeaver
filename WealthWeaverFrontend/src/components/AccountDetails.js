import React from "react";
import Transactions from "./Transactions";

/**
 * This component shows the account details
 * * Account Summary view
 * * Recent Transactions view
 *
 * @param accountDetails @see mock-data/accountDetails.json
 *
 */
export default function AccountDetails({ accountDetails }) {
  return (
    <div className="rounded-xl bg-white shadow-lg dark:bg-gray-700 text-white">
      <div className="flex rounded-tl-xl gap-4 flex-col">
        <div className="flex-1 bg-blue-500 px-4 py-8 rounded-t-xl shadow">
          <span className="bg-blue-700 px-4 py-1 rounded-full text-xs uppercase">
            {accountDetails.bankName}
          </span>
          <h1 className="text-2xl mt-2"> {accountDetails.accountName}</h1>
          <p className="text-sm"> x {accountDetails.last4}</p>
        </div>
        <div className="flex-1 text-sm text-gray-500 dark:text-gray-300 mr-4 px-4">
          <ul className="flex flex-col gap-2">
            <li className="flex items-center">
              <span className="flex-grow">Actual Balance</span>
              <span className="text-2xl text-black dark:text-white font-bold">
                {accountDetails.balance}
              </span>
            </li>
            <li className="flex items-center">
              <span className="flex-grow">Available Balance</span>
              <span>{accountDetails.balance}</span>
            </li>
          </ul>
        </div>
      </div>
      <div className="my-8 mx-4">
        <h1 className="uppercase text-gray-500 text-left">
          RECENT Transactions
        </h1>
        <Transactions transactions={accountDetails.recentTransactions} />
      </div>
    </div>
  );
}
