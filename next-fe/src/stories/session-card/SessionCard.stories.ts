import { Meta, StoryObj } from "@storybook/react";
import { SessionCard } from "../../components";
import { Session } from "@/shared/types/Conference";
import { testSession } from "./dummyData";

const meta: Meta<typeof SessionCard> = {
  title: "Components/SessionCard",
  component: SessionCard,
  tags: ["autodocs"],
  argTypes: {
    session: {
      name: "Session",
      defaultValue: testSession,
    },
  },
};

export default meta;

type Story = StoryObj<typeof SessionCard>;

export const Default: Story = {
  args: { session: testSession },
};
