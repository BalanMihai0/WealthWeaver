import React from "react";
import AccountSelector from "../components/AccountSelector";
import Header from "../components/Header";
import Transactions from "../components/Transactions";
import transactions from "../mock-data/transactions.json";
import accounts from "../mock-data/bankAccounts.json";

/**
 * This Page shows the transactions for a selected account
 * Provides two section to recent transactions  and
 * past transactions with a filter by dates component
 */
export default function TransactionsPage() {
  return (
    <main className="px-4 py-8 ">
      <Header active={"transactions"} />
      <div className="flex flex-col">
        <div className="flex justify-center">
          <AccountSelector
            accounts={[...accounts.bankAccounts, ...accounts.creditCards]}
          />
        </div>

        <div className="pb-8 md:w-2/3 self-center">
          <h1 className="text-gray-500 my-4 text-center">
            RECENT TRANSACTIONS
          </h1>
          <div className="bg-white rounded-lg p-4 shadow-lg dark:bg-gray-700">
            <Transactions transactions={transactions.recentTransactions} />
          </div>
          <h1 className="text-gray-500 my-4 mt-8 text-center">
            PAST TRANSACTIONS
          </h1>
          <div className="bg-gray-200 flex flex-col items-center justify-around lg:flex-row p-4 mb-4 rounded-lg dark:bg-gray-800 dark:text-white">
            <div>
              <label htmlFor="startDate">Start Date - </label>
              <input
                id="startDate"
                type="date"
                className="bg-transparent focus:outline-none"
              />
            </div>
            <div>
              <label htmlFor="endDate">End Date - </label>
              <input
                id="endDate"
                type="date"
                className="bg-transparent focus:outline-none"
              />
            </div>
          </div>
          <div className="bg-white rounded-lg p-4 shadow-lg dark:bg-gray-700">
            <Transactions transactions={transactions.pastTransactions} />
            <div className="flex justify-center">
              <div className="flex gap-4 rounded-full bg-gray-100 px-4 py-2 cursor-pointer dark:bg-gray-800 dark:text-white">
                <span className="rounded-full bg-blue-500 px-4 text-white">
                  1
                </span>
                <span>2</span>
                <span>...</span>
                <span>10</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
