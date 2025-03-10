import React from "react";
import Header from "../components/Header";
import AccountDetails from "../components/AccountDetails";
import AccountSummary from "../components/AccountSummary";
import accounts from "../mock-data/bankAccounts.json";
import accountDetails from "../mock-data/accountDetails.json";
import { PlusCircleIcon } from "@heroicons/react/outline";

/**
 * This page contains a summary of all the Accounts and
 * show the details of a selected account
 *
 */
export default function AccountsPage() {
  return (
    <main className="px-4 py-8">
      <Header active={"accounts"} />
      <div className="flex gap-8 flex-col lg:flex-row">
        <div className="flex flex-col gap-12">
          <div>
            <h1 className="text-gray-500 mb-12 text-center">ACCOUNTS</h1>
            <section className="flex gap-8 flex-wrap justify-center md:justify-start">
              {accounts.bankAccounts.map((bank) => (
                <AccountSummary
                  account={bank}
                  color={bank.color}
                  key={bank.id}
                />
              ))}
              <div className="h-40 w-64 rounded-xl cursor-pointer border-2 border-dashed border-gray-300 flex items-center justify-center flex-col gap-2 dark:border-gray-700">
                <PlusCircleIcon className="w-20 h-20 text-gray-300 dark:text-gray-500" />
                <span className="text-xs dark:text-gray-300">
                  Add New Account
                </span>
              </div>
            </section>
          </div>
          <div>
            <h1 className="text-gray-500 mb-12 text-center">CARDS</h1>
            <section className="flex gap-8 flex-wrap justify-center md:justify-start">
              {accounts.creditCards.map((card) => (
                <AccountSummary
                  account={card}
                  color={card.color}
                  key={card.id}
                />
              ))}
              <div className="h-40 w-64 rounded-xl cursor-pointer border-2 border-dashed border-gray-300 flex items-center justify-center flex-col gap-2 dark:border-gray-700">
                <PlusCircleIcon className="w-20 h-20 text-gray-300 dark:text-gray-500" />
                <span className="text-xs dark:text-gray-300">Add New Card</span>
              </div>
            </section>
          </div>
        </div>
        <div className="mt-8 lg:mt-0 flex-1">
          <AccountDetails accountDetails={accountDetails} />
        </div>
      </div>
    </main>
  );
}
